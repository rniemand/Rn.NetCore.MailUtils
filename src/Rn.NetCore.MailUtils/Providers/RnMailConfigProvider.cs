using Rn.NetCore.MailUtils.Config;

namespace Rn.NetCore.MailUtils.Providers;

// DOCS: docs\providers\RnMailConfigProvider.md
public interface IRnMailConfigProvider
{
  RnMailConfig GetRnMailConfig();
}

public class RnMailConfigProvider : IRnMailConfigProvider
{
  public RnMailConfig GetRnMailConfig()
  {
    return new RnMailConfig();
  }
}
