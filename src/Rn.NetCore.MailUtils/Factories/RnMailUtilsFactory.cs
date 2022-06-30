using Rn.NetCore.MailUtils.Builders;

namespace Rn.NetCore.MailUtils;

public interface IRnMailUtilsFactory
{
  MailMessageBuilder Create();
}

public class RnMailUtilsFactory : IRnMailUtilsFactory
{
  private readonly RnMailConfig _config;

  public RnMailUtilsFactory(RnMailConfig config)
  {
    _config = config;
  }

  public MailMessageBuilder Create() =>
    new MailMessageBuilder().WithFrom(_config);
}
