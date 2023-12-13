namespace FileTransferService.Services.Interfaces;

/// <summary>
/// Сервис для работ с файлом.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Объединить части файла.
    /// </summary>
    /// <param name="filePath">Путь до файла.</param>
    /// <param name="cancellationToken">Токен отмены выполнения операции.</param>
    Task MergeFileAsync(string filePath, CancellationToken cancellationToken);

    /// <summary>
    /// Отправить части файла.
    /// </summary>
    /// <param name="fileName">Имя отправляемоего файла.</param>
    /// <param name="cancellationToken">Токен отмены выполнения операции.</param>
    Task SendFileAsync(string fileName, CancellationToken cancellationToken);

    /// <summary>
    /// Разделить файл на части.
    /// </summary>
    /// <param name="filePath">Путь до файла.</param>
    /// <param name="cancellationToken">Токен отмены выполнения операции.</param>
    Task SplitFileAsync(string filePath, CancellationToken cancellationToken);
}