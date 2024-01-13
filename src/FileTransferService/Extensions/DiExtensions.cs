namespace FileTransferService.Extensions;

using FileTransferService.Middlewares;
using FileTransferService.Options;
using FileTransferService.Services.Implementations;
using FileTransferService.Services.Interfaces;

using Serilog;

/// <summary>
/// Расширения для DI.
/// </summary>
public static class DiExtensions
{
    /// <summary>
    /// Добавить промежуточные слои приложения.
    /// </summary>
    /// <param name="builder">Строитель приложения.</param>
    public static void AddAppMiddlewares(this IApplicationBuilder builder)
    {
        Log.Logger.Information("Добавление промежуточных слоев приложения");

        builder.UseMiddleware<ExceptionHandlingMiddleware>();

        Log.Logger.Information("Промежуточные слои приложения добавлены");
    }

    /// <summary>
    /// Добавить HttpClient.
    /// </summary>
    /// <param name="builder">Строитель веб приложения.</param>
    /// <exception cref="InvalidOperationException">Когда не указан uri Сервиса объединения.</exception>
    public static void AddHttpClient(this WebApplicationBuilder builder)
    {
        Log.Logger.Information("Добавление HttpClient");
        
        var mergeServiceHost = builder.Configuration.GetSection(nameof(MergeServiceOptions))
                                      .GetSection("MergeServiceHost").Value;
        var mergeServiceUri = new Uri(mergeServiceHost!);

        builder.Services.AddHttpClient<IFileService, FileService>(httpClient =>
                   httpClient.BaseAddress = mergeServiceUri)
               .AddRetryPolicy();
        
        Log.Logger.Information("HttpClient добавлен");
    }

    /// <summary>
    /// Настроить конструктор опций.
    /// </summary>
    /// <param name="builder">Строитель веб приложения.</param>
    public static void AddOptions(this WebApplicationBuilder builder)
    {
        Log.Logger.Information("Добавление настроек");
            
        builder.Services.AddOptions<FilesOptions>().Bind(builder.Configuration.GetSection(nameof(FileOptions)))
               .ValidateDataAnnotations()
               .ValidateOnStart();

        builder.Services.AddOptions<MergeServiceOptions>()
               .Bind(builder.Configuration.GetSection(nameof(MergeServiceOptions)))
               .ValidateDataAnnotations()
               .ValidateOnStart();
        
        Log.Logger.Information("Настройки добавлены");
    }
}