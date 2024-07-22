using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace Dr.ActionHero.Extensions;

public static class LoggerExtensions
{
    public static IHostBuilder AddInMemoryLogger(
        this IHostBuilder builder,
        Action<InMemoryLoggerConfiguration> configuration)
    {
        builder.ConfigureLogging(logBuilder =>
        {
            logBuilder.AddInMemoryLogger(configuration);
        });

        return builder;
    }

    public static ILoggingBuilder AddInMemoryLogger(
        this ILoggingBuilder builder,
        Action<InMemoryLoggerConfiguration> configuration)
    {
        builder.ClearProviders();
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, InMemoryLoggerProvider>());

        builder.Services.Configure(configuration);

        LoggerProviderOptions.RegisterProviderOptions
            <InMemoryLoggerConfiguration, InMemoryLoggerProvider>(builder.Services);

        return builder;
    }
}
