namespace FileTransferService.Services.Interfaces;

/// <summary>
/// Сервис для работ с файлом.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Разделить файл на части.
    /// </summary>
    /// <param name="filePath">Путь до файла.</param>
    /// <param name="cancellationToken">Токен отмены выполнения операции.</param>
    Task SplitFileAsync(string filePath, CancellationToken cancellationToken);
}