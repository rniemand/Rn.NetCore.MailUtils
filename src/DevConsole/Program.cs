using DevConsole;
using Microsoft.Extensions.DependencyInjection;
using Rn.NetCore.MailUtils.Factories;

var smtpClientFactory = DevDIContainer.ServiceProvider
  .GetRequiredService<ISmtpClientFactory>();

Console.WriteLine();

