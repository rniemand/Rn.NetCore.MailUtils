using Microsoft.Extensions.Configuration;
using Rn.NetCore.Common.Logging;

namespace Rn.NetCore.MailUtils;

public interface IRnMailConfigProvider
{
  RnMailConfig GetRnMailConfig();
}

public class RnMailConfigProvider : IRnMailConfigProvider
{
  public const string Key = "Rn.MailUtils";
  private readonly ILoggerAdapter<RnMailConfigProvider> _logger;
  private readonly RnMailConfig _config;

  public RnMailConfigProvider(
    ILoggerAdapter<RnMailConfigProvider> logger,
    IConfiguration configuration)
  {
    _logger = logger;
    _config = BindConfiguration(configuration);
  }

  public RnMailConfig GetRnMailConfig() => _config;

  private RnMailConfig BindConfiguration(IConfiguration configuration)
  {
    var boundConfig = new RnMailConfig();
    var configSection = configuration.GetSection(Key);

    if (!configSection.Exists())
    {
      _logger.LogError("Unable to find mail configuration at: {key}", Key);
      return boundConfig;
    }

    _logger.LogInformation("Found mail configuration at: {key}", Key);
    configSection.Bind(boundConfig);
    return boundConfig;
  }
}
