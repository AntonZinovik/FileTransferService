namespace FileTransferService.Services.Implementations;

using System.IO.Compression;

using FileTransferService.Options;
using FileTransferService.Services.Interfaces;

using Microsoft.Extensions.Options;

/// <summary>
/// Сервис для работ с файлами.
/// </summary>
public class FileService : IFileService
{
    /// <summary>
    /// Настройки файлов.
    /// </summary>
    private readonly FilesOptions _filesOptions;

    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<FileService> _logger;

    /// <inheritdoc cref="IFileService"/>
    /// <param name="logger">Логгер.</param>
    /// <param name="splitOptions">Настройки файлов.</param>
    public FileService(ILogger<FileService> logger, IOptions<FilesOptions> splitOptions)
    {
        _logger = logger;
        _filesOptions = splitOptions.Value;
    }

    /// <inheritdoc cref="IFileService.SplitFile"/>
    public async Task SplitFile(string filePath, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Процесс разделения файла на части начался");

        await using var fileStream = File.OpenRead(filePath);

        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
        var pathToDirectoryChunks = Path.Combine(_filesOptions.PathChunkDirectory, fileNameWithoutExtension);
        Directory.CreateDirectory(pathToDirectoryChunks);
        _logger.LogInformation("Создана директория для частей файла:{PathToDirectoryChunks}", pathToDirectoryChunks);

        int bytesRead;
        var chunkNumber = 0;
        var buffer = new byte[_filesOptions.BufferSize];

        while ((bytesRead = await fileStream.ReadAsync(buffer, cancellationToken)) > 0)
        {
            chunkNumber++;

            var nameChunkFile = Path.Combine(pathToDirectoryChunks,
                chunkNumber + ".gz");

            await using var targetStream = File.Create(nameChunkFile);
            await using var compressionStream = new GZipStream(targetStream, CompressionMode.Compress);
            await compressionStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
        }

        _logger.LogInformation("Процесс разделения файла успешно завершен");
    }
}