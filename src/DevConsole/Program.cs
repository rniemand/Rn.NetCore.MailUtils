using DevConsole;
using Microsoft.Extensions.DependencyInjection;
using Rn.NetCore.MailUtils.Providers;

var configProvider = DevDIContainer.ServiceProvider
  .GetRequiredService<IRnMailConfigProvider>();

var config = configProvider.GetRnMailConfig();

Console.WriteLine();

