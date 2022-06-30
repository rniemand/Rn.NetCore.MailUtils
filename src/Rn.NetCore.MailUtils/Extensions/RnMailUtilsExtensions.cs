using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Logging;

namespace Rn.NetCore.MailUtils;

public static class RnMailUtilsExtensions
{
  public static IServiceCollection AddRnMailUtils(this IServiceCollection services)
  {
    // Optional service registration
    services.TryAddSingleton<IEnvironmentAbstraction, EnvironmentAbstraction>();
    services.TryAddSingleton<IPathAbstraction, PathAbstraction>();
    services.TryAddSingleton<IDirectoryAbstraction, DirectoryAbstraction>();
    services.TryAddSingleton<IFileAbstraction, FileAbstraction>();
    services.TryAddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

    return services
      .AddSingleton<ISmtpClientFactory, SmtpClientFactory>()
      .AddSingleton<IMailMessageBuilderFactory, MailMessageBuilderFactory>()
      .AddSingleton<IRnMailConfigProvider, RnMailConfigProvider>()
      .AddSingleton<IMailTemplateProvider, MailTemplateProvider>()
      .AddSingleton<IMailTemplateHelper, MailTemplateHelper>();
  }
}
