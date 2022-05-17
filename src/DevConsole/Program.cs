using DevConsole;
using Microsoft.Extensions.DependencyInjection;
using Rn.NetCore.MailUtils.Factories;
using Rn.NetCore.MailUtils.Helpers;
using Rn.NetCore.MailUtils.Providers;

var rnMailConfig = DevDIContainer.ServiceProvider
  .GetRequiredService<IRnMailConfigProvider>()
  .GetRnMailConfig();

var smtpClient = DevDIContainer.ServiceProvider
  .GetRequiredService<ISmtpClientFactory>()
  .Create();

var messageBuilder = DevDIContainer.ServiceProvider
  .GetRequiredService<IMailMessageBuilderFactory>()
  .Create();

var templateHelper = DevDIContainer.ServiceProvider
  .GetRequiredService<IMailTemplateHelper>();

var builder = templateHelper.GetTemplateBuilder("testing");


Console.WriteLine();
Console.WriteLine();

