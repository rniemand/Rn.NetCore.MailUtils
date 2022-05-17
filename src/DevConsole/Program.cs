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

var templateBuilder = templateHelper.GetTemplateBuilder("testing")
  .AddPlaceHolder("name", "Richard Niemand")
  .AddPlaceHolder("currentDate", DateTime.Now)
  .AddPlaceholders(new Dictionary<string, object>
  {
    {"hello", "World"},
    {"int", 23},
    {"long", (long) 12},
    {"bool", true},
    {"double", 12.2},
    {"float", (float) 12}
  });

var mailMessage = messageBuilder
  .WithTo(rnMailConfig.FromAddress)
  .WithHtmlBody(templateBuilder)
  .WithSubject("Hello world")
  .Build();

// await smtpClient.SendMailAsync(mailMessage);

Console.WriteLine();
Console.WriteLine();
