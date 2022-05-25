using Rn.NetCore.Common.Logging;
using Rn.NetCore.MailUtils.Builders;
using Rn.NetCore.MailUtils.Providers;

namespace Rn.NetCore.MailUtils.Factories;

public interface IMailMessageBuilderFactory
{
  MailMessageBuilder Create();
}

public class MailMessageBuilderFactory : IMailMessageBuilderFactory
{
  private readonly ILoggerAdapter<MailMessageBuilderFactory> _logger;
  private readonly IRnMailConfigProvider _mailConfigProvider;

  public MailMessageBuilderFactory(
    ILoggerAdapter<MailMessageBuilderFactory> logger,
    IRnMailConfigProvider mailConfigProvider)
  {
    _logger = logger;
    _mailConfigProvider = mailConfigProvider;
  }

  public MailMessageBuilder Create() =>
    new MailMessageBuilder().WithFrom(_mailConfigProvider.GetRnMailConfig());
}
