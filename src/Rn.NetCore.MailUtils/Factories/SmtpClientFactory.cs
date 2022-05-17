using System.Net;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.MailUtils.Providers;
using Rn.NetCore.MailUtils.Wrappers;

namespace Rn.NetCore.MailUtils.Factories;

// DOCS: docs\factories\SmtpClientFactory.md
public interface ISmtpClientFactory
{
  ISmtpClient Create();
}

public class SmtpClientFactory : ISmtpClientFactory
{
  private readonly ILoggerAdapter<SmtpClientFactory> _logger;
  private readonly IRnMailConfigProvider _configProvider;

  public SmtpClientFactory(
    ILoggerAdapter<SmtpClientFactory> logger,
    IRnMailConfigProvider configProvider)
  {
    _logger = logger;
    _configProvider = configProvider;
  }

  public ISmtpClient Create()
  {
    // TODO: [SmtpClientFactory.Create] (TESTS) Add tests
    var config = _configProvider.GetRnMailConfig();
    var smtpClient = new SmtpClientWrapper(config.Host, config.Port)
    {
      DeliveryFormat = config.DeliveryFormat,
      DeliveryMethod = config.DeliveryMethod,
      EnableSsl = config.EnableSsl,
      PickupDirectoryLocation = null,
      TargetName = null,
      Timeout = config.Timeout,
      UseDefaultCredentials = false
    };

    if (config.HasCredentials())
    {
      smtpClient.Credentials = new NetworkCredential(config.Username, config.Password);
    }

    _logger.LogDebug("Created new instance for: {host}", config.Host);
    return smtpClient;
  }
}
