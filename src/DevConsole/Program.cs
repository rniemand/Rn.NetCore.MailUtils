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

var mailMessage = messageBuilder
  .WithTo(rnMailConfig.FromAddress, rnMailConfig.FromName)
  .WithSubject("Test email")
  .WithHtmlBody("This is meant to be HTML")
  .Build();

// await smtpClient.SendMailAsync(mailMessage);


Console.WriteLine();
Console.WriteLine();

