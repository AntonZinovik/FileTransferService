namespace FileTransferService.Services.Implementations;

using System.IO.Compression;
using System.Text;

using FileTransferService.Dtos;
using FileTransferService.Extensions;
using FileTransferService.Options;
using FileTransferService.Services.Interfaces;

using Microsoft.Extensions.Options;

/// <summary>
/// Сервис для работ с файлами.
/// </summary>
public class FileService : IFileService
{
    /// <summary>
    /// <inheritdoc cref="FilesOptions"/>
    /// </summary>
    private readonly FilesOptions _filesOptions;

    /// <summary>
    /// <inheritdoc cref="HttpClient"/>
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<FileService> _logger;

    /// <summary>
    /// <inheritdoc cref="MergeServiceOptions"/>
    /// </summary>
    private readonly MergeServiceOptions _mergeServiceOptions;

    /// <inheritdoc cref="IFileService"/>
    /// <param name="logger">Логгер.</param>
    /// <param name="splitOptions">Настройки файлов.</param>
    /// <param name="httpClient">Http клиент.</param>
    /// <param name="mergeServiceOptions">Настройки конечных точек сервиса объединения.</param>
    /// <exception cref="ArgumentNullException">Когда указанны некорректные настройки файлов.</exception>
    public FileService(ILogger<FileService> logger, IOptions<FilesOptions> splitOptions, HttpClient httpClient,
        IOptions<MergeServiceOptions> mergeServiceOptions)
    {
        _logger = logger;
        _httpClient = httpClient;
        _mergeServiceOptions = mergeServiceOptions.Value;
        _filesOptions = splitOptions.Value;
    }

    /// <inheritdoc cref="IFileService.MergeFileAsync"/>
    public async Task MergeFileAsync(string filePath, CancellationToken cancellationToken)
    {
        var fileName = Path.GetFileName(filePath);
        await using var fileStream = File.OpenRead(filePath);
        var hashCode = await fileStream.CalculationHashCodeAsync();

        var dto = new FileDto(fileName, hashCode);

        _logger.LogInformation("Отправлен запрос на объединение чанков файла:{FileName}", fileName);

        var response =
            await _httpClient.PostAsJsonAsync(_mergeServiceOptions.MergeFileEndPoint, dto, cancellationToken);

        await response.GetExceptionsAsync(cancellationToken);
    }

    /// <inheritdoc cref="IFileService.SendFileAsync"/>
    public async Task SendFileAsync(string fileName, CancellationToken cancellationToken)
    {
        var directory = Path.Combine(_filesOptions.PathChunkDirectory!, fileName);

        if (!Directory.Exists(directory))
        {
            throw new NullReferenceException("Не существует папки с частями указанного файла.");
        }

        // Сортируем все чанки в правильном порядке
        var chunks = Directory.GetFiles(directory)
                              .OrderBy(chunk => int.Parse(Path.GetFileNameWithoutExtension(chunk)));

        foreach (var chunk in chunks)
        {
            await Task.Factory.StartNew(async () => await SendChunkAsync(fileName, chunk, cancellationToken),
                cancellationToken);
        }
    }

    /// <inheritdoc cref="IFileService.SplitFileAsync"/>
    public async Task SplitFileAsync(string filePath, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Процесс разделения файла на части начался");

        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
        var pathToDirectoryChunks = Path.Combine(_filesOptions.PathChunkDirectory!, fileNameWithoutExtension);

        if (!Directory.Exists(pathToDirectoryChunks))
        {
            Directory.CreateDirectory(pathToDirectoryChunks);

            _logger.LogInformation("Создана директория для частей файла:{PathToDirectoryChunks}",
                pathToDirectoryChunks);
        }

        var chunkNumber = 0;
        var buffer = new byte[_filesOptions.BufferSize];
        int bytesRead;
        await using var fileStream = File.OpenRead(filePath);

        while ((bytesRead = await fileStream.ReadAsync(buffer, cancellationToken)) > 0)
        {
            chunkNumber++;

            var nameChunkFile = Path.Combine(pathToDirectoryChunks,
                chunkNumber + ".gz");

            await using var targetStream = File.Create(nameChunkFile);
            await using var compressionStream = new GZipStream(targetStream, CompressionMode.Compress);
            var memory = buffer.AsMemory(0, bytesRead);
            await compressionStream.WriteAsync(memory, cancellationToken);
        }

        _logger.LogInformation("Процесс разделения файла успешно завершен");
    }

    /// <summary>
    /// Отправить часть файла.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены выполнения операции.</param>
    /// <param name="fileName">Имя файла.</param>
    /// <param name="chunkPath">Путь до части файла.</param>
    /// <returns>Http сообщение.</returns>
    private async Task SendChunkAsync(string fileName, string chunkPath, CancellationToken cancellationToken)
    {
        await using var sourceStream = new FileStream(chunkPath, FileMode.Open);
        using var reader = new BinaryReader(sourceStream, Encoding.UTF8, true);
        var bytes = reader.ReadBytes((int)sourceStream.Length);
        var hashCode = await sourceStream.CalculationHashCodeAsync();
        var dto = new ChunkDto(fileName, bytes, hashCode, Path.GetFileName(chunkPath));

        var response =
            await _httpClient.PostAsJsonAsync(_mergeServiceOptions.SaveChunkEndpoint, dto, cancellationToken);
        await response.GetExceptionsAsync(cancellationToken);

        sourceStream.Close();
        File.Delete(chunkPath);
    }
}