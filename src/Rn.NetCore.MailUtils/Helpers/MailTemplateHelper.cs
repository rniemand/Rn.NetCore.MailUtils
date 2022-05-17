using Rn.NetCore.Common.Logging;
using Rn.NetCore.MailUtils.Builders;
using Rn.NetCore.MailUtils.Providers;

namespace Rn.NetCore.MailUtils.Helpers;

public interface IMailTemplateHelper
{
  MailTemplateBuilder GetTemplateBuilder(string templateName);
}

public class MailTemplateHelper : IMailTemplateHelper
{
  private readonly ILoggerAdapter<MailTemplateHelper> _logger;
  private readonly IMailTemplateProvider _templateProvider;

  public MailTemplateHelper(
    ILoggerAdapter<MailTemplateHelper> logger,
    IMailTemplateProvider templateProvider)
  {
    _logger = logger;
    _templateProvider = templateProvider;
  }

  public MailTemplateBuilder GetTemplateBuilder(string templateName)
  {
    // TODO: [MailTemplateHelper.GetTemplateBuilder] (TESTS) Add tests

    Console.WriteLine();
    return new MailTemplateBuilder();
  }
}
