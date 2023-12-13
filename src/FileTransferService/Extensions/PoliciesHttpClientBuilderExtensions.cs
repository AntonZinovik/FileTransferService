namespace FileTransferService.Extensions;

using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Timeout;

/// <summary>
/// Методы расширения для конфигурирования  <see cref="IHttpClientBuilder"/> с политиками Polly повторных попыток.
/// </summary>
public static class PoliciesHttpClientBuilderExtensions
{
    /// <summary>
    /// Добавление политики повторных запросов.
    /// </summary>
    /// <param name="clientBuilder">Строитель http клиентов.</param>
    public static void AddRetryPolicy(this IHttpClientBuilder clientBuilder)
    {
        var delay = Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(2), 5);

        clientBuilder
            .AddPolicyHandler(HttpPolicyExtensions
                              .HandleTransientHttpError()
                              .Or<TimeoutRejectedException>()
                              .WaitAndRetryAsync(delay));
    }
}