using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.MailUtils.Extensions;

namespace DevConsole;

internal static class DevDIContainer
{
  public static IServiceProvider ServiceProvider { get; }
  private static string _mailSettings = "\\\\192.168.0.60\\appdata\\cron-tool\\mail-settings.json";

  static DevDIContainer()
  {
    ServiceProvider = Configure();
  }

  private static IServiceProvider Configure()
  {
    var services = new ServiceCollection();

    var configBuilderRoot = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", true, true);

    if (File.Exists(_mailSettings))
      configBuilderRoot.AddJsonFile(_mailSettings);

    var config = configBuilderRoot.Build();

    services
      // Configuration
      .AddSingleton<IConfiguration>(config)

      // Abstractions
      .AddSingleton<IEnvironmentAbstraction, EnvironmentAbstraction>()
      .AddSingleton<IPathAbstraction, PathAbstraction>()
      .AddSingleton<IDirectoryAbstraction, DirectoryAbstraction>()
      .AddSingleton<IFileAbstraction, FileAbstraction>()

      // Logging
      .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
      .AddLogging(loggingBuilder =>
      {
        // configure Logging with NLog
        loggingBuilder.ClearProviders();
        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
        loggingBuilder.AddNLog(config);
      })

      // RnMailUtils
      .AddRnMailUtils();

    return services.BuildServiceProvider();
  }
}
