namespace FileTransferService.Controllers;

using FileTransferService.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Котроллер для работы с файлами.
/// </summary>
[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{
    /// <inheritdoc cref="IFileService"/>
    private readonly IFileService _fileService;

    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<FilesController> _logger;

    /// <inheritdoc cref="FilesController"/>
    /// <param name="logger">Логгер.</param>
    /// <param name="fileService">Сервис для работ с файлом.</param>
    public FilesController(ILogger<FilesController> logger, IFileService fileService)
    {
        _logger = logger;
        _fileService = fileService;
    }

    /// <summary>
    /// Объединение частей файла.
    /// </summary>
    /// <param name="filePath">Путь до файла.</param>
    /// <param name="cancellationToken">Токен отмены выполнения операции.</param>
    /// <exception cref="ArgumentNullException">Когда путь до файла оказался пустым.</exception>
    /// <response code="204">Когда удалось объединить файл.</response>
    [HttpPost("Merge")]
    public async Task<NoContentResult> MergeFiles([FromBody] string filePath,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentNullException(filePath, "Параметр не корректный");
        }

        _logger.LogInformation("Передан корректный путь до файла");
        await _fileService.MergeFileAsync(filePath, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Отправка частей файла.
    /// </summary>
    /// <param name="fileName">Имя отправляемого файла.</param>
    /// <param name="cancellationToken">Токен отмены выполнения операции.</param>
    /// <exception cref="ArgumentNullException">Когда имя файла оказалось пустым.</exception>
    /// <response code="204">Когда удалось отправить части файла.</response>
    [HttpPost("Send")]
    public async Task<NoContentResult> SendChunkFiles([FromBody] string fileName,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentNullException(fileName, "Параметр не корректный");
        }

        _logger.LogInformation("Передан корректное имя файла");
        await _fileService.SendFileAsync(fileName, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Разделение файла на части.
    /// </summary>
    /// <param name="filePath">Путь до файла.</param>
    /// <param name="cancellationToken">Токен отмены выполнения операции.</param>
    /// <exception cref="ArgumentNullException">Когда путь до файла оказался пустым.</exception>
    /// <response code="204">Когда удалось разделить файл.</response>
    [HttpPost("Split")]
    public async Task<NoContentResult> SplitFile([FromBody] string filePath,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentNullException(filePath, "Параметр не корректный");
        }

        _logger.LogInformation("Передан корректный путь до файла");
        await _fileService.SplitFileAsync(filePath, cancellationToken);

        return NoContent();
    }
}