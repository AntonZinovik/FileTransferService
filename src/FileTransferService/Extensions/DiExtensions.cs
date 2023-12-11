namespace FileTransferService.Extensions;

using FileTransferService.Options;
using FileTransferService.Services.Implementations;
using FileTransferService.Services.Interfaces;

/// <summary>
/// Расширения для DI.
/// </summary>
public static class DiExtensions
{
    /// <summary>
    /// Добавить HttpClient.
    /// </summary>
    /// <param name="builder">Строитель веб приложения.</param>
    /// <exception cref="InvalidOperationException">Когда не указан uri Сервиса объединения.</exception>
    public static void AddHttpClient(this WebApplicationBuilder builder)
    {
        var mergeServiceUri = new Uri(builder.Configuration.GetSection("MergeServiceUri").Value ??
                                      throw new InvalidOperationException("Не корректно указан MergeServiceUri"));

        builder.Services.AddHttpClient<IFileService, FileService>(httpClient =>
                   httpClient.BaseAddress = mergeServiceUri)
               .AddRetryPolicy();
    }

    /// <summary>
    /// Настроить конструктор опций.
    /// </summary>
    /// <param name="builder">Строитель веб приложения.</param>
    public static void AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<FilesOptions>().Bind(builder.Configuration.GetSection(nameof(FileOptions)))
               .ValidateDataAnnotations()
               .ValidateOnStart();

        builder.Services.AddOptions<MergeServiceOptions>()
               .Bind(builder.Configuration.GetSection(nameof(MergeServiceOptions)))
               .ValidateDataAnnotations()
               .ValidateOnStart();
    }
}