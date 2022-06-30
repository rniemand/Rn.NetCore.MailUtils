using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Rn.NetCore.MailUtils;

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
      .AddLogging(loggingBuilder =>
      {
        loggingBuilder.ClearProviders();
        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
        loggingBuilder.AddNLog(config);
      })
      .AddRnMailUtils(config);

    return services.BuildServiceProvider();
  }
}
