using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Rn.NetCore.MailUtils.Config;

// DOCS: docs\configuration\RnMailConfig.md
public class RnMailConfig
{
  [ConfigurationKeyName("host")]
  public string Host { get; set; } = "smtp.gmail.com";

  [ConfigurationKeyName("port")]
  public int Port { get; set; } = 587;

  [ConfigurationKeyName("username")]
  public string Username { get; set; } = "";

  [ConfigurationKeyName("password")]
  public string Password { get; set; } = "";

  [ConfigurationKeyName("fromAddress")]
  public string FromAddress { get; set; } = "";

  [ConfigurationKeyName("fromName")]
  public string FromName { get; set; } = "";

  [ConfigurationKeyName("deliveryFormat")]
  public SmtpDeliveryFormat DeliveryFormat { get; set; } = SmtpDeliveryFormat.SevenBit;

  [ConfigurationKeyName("deliveryMethod")]
  public SmtpDeliveryMethod DeliveryMethod { get; set; } = SmtpDeliveryMethod.Network;

  [ConfigurationKeyName("enableSsl")]
  public bool EnableSsl { get; set; } = true;

  [ConfigurationKeyName("timeout")]
  public int Timeout { get; set; } = 30000;

  public bool HasCredentials()
  {
    // TODO: [RnMailConfig.HasCredentials] (TESTS) Add tests
    if (string.IsNullOrWhiteSpace(Username))
      return false;

    return true;
  }
}
