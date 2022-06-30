using Rn.NetCore.MailUtils.Builders;

namespace Rn.NetCore.MailUtils;

public interface IMailMessageBuilderFactory
{
  MailMessageBuilder Create();
}

public class MailMessageBuilderFactory : IMailMessageBuilderFactory
{
  private readonly RnMailConfig _config;

  public MailMessageBuilderFactory(RnMailConfig config)
  {
    _config = config;
  }

  public MailMessageBuilder Create() =>
    new MailMessageBuilder().WithFrom(_config);
}
