namespace FileTransferService.Options;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Настройки конечных точек сервиса объединения.
/// </summary>
public class MergeServiceOptions
{
    /// <summary>
    /// Конечная точка запроса объединения файла.
    /// </summary>
    [Required(ErrorMessage = "Указан не корректная точка запроса объединения файла.")]
    public string? MergeFileEndPoint { get; init; }

    /// <summary>
    /// Конечная точка запроса сохранения чанка.
    /// </summary>
    [Required(ErrorMessage = "Указан не корректная точка запроса сохранения чанка.")]
    public string? SaveChunkEndpoint { get; init; }
}