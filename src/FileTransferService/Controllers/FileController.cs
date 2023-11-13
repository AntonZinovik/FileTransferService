namespace FileTransferService.Controllers;

using FileTransferService.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Котроллер для работы с файлом.
/// </summary>
[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    /// <inheritdoc cref="IFileService"/>
    private readonly IFileService _fileService;

    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<FileController> _logger;

    /// <inheritdoc cref="FileController"/>
    /// <param name="logger">Логгер.</param>
    /// <param name="fileService">Сервис для работ с файлом.</param>
    public FileController(ILogger<FileController> logger, IFileService fileService)
    {
        _logger = logger;
        _fileService = fileService;
    }

    /// <summary>
    /// Разделеие файла на части.
    /// </summary>
    /// <param name="filePath">Логгер.</param>
    /// <param name="cancellationToken">Токен отмены выполнения операции.</param>
    /// <exception cref="ArgumentNullException">Когда путь до файла оказался пустым.</exception>
    /// <response code="204">Когда удалось разделить файл.</response>
    [HttpPost("File")]
    public async Task<NoContentResult> SplitFile([FromBody] string filePath,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentNullException(filePath);
        }

        _logger.LogInformation("Передан корректный путь до файла");
        await _fileService.SplitFile(filePath, cancellationToken);

        return NoContent();
    }
}