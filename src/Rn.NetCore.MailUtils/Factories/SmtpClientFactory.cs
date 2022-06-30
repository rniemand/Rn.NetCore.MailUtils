using System.Net;
using Rn.NetCore.Common.Logging;

namespace Rn.NetCore.MailUtils;

public interface ISmtpClientFactory
{
  ISmtpClient Create();
}

public class SmtpClientFactory : ISmtpClientFactory
{
  private readonly ILoggerAdapter<SmtpClientFactory> _logger;
  private readonly RnMailConfig _mailConfig;

  public SmtpClientFactory(
    ILoggerAdapter<SmtpClientFactory> logger,
    RnMailConfig mailConfig)
  {
    _logger = logger;
    _mailConfig = mailConfig;
  }

  public ISmtpClient Create()
  {
    var smtpClient = new SmtpClientWrapper(_mailConfig.Host, _mailConfig.Port)
    {
      DeliveryFormat = _mailConfig.DeliveryFormat,
      DeliveryMethod = _mailConfig.DeliveryMethod,
      EnableSsl = _mailConfig.EnableSsl,
      PickupDirectoryLocation = null,
      TargetName = null,
      Timeout = _mailConfig.Timeout,
      UseDefaultCredentials = false
    };

    if (_mailConfig.HasCredentials())
    {
      smtpClient.Credentials = new NetworkCredential(_mailConfig.Username, _mailConfig.Password);
    }

    _logger.LogDebug("Created new instance for: {host}", _mailConfig.Host);
    return smtpClient;
  }
}
