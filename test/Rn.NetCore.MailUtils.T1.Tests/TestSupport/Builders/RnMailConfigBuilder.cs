namespace Rn.NetCore.MailUtils.T1.Tests.TestSupport.Builders;

public class RnMailConfigBuilder
{
  public static RnMailConfig Default = new RnMailConfigBuilder().WithDefaults().Build();

  private readonly RnMailConfig _mailConfig = new();

  public RnMailConfigBuilder WithDefaults()
  {
    _mailConfig.Username = "username";
    _mailConfig.Password = "password";

    return WithFromName("From Name")
      .WithFromAddress("from@address.com");
  }

  public RnMailConfigBuilder WithFromAddress(string fromAddress)
  {
    _mailConfig.FromAddress = fromAddress;
    return this;
  }

  public RnMailConfigBuilder WithFromName(string fromName)
  {
    _mailConfig.FromName = fromName;
    return this;
  }

  public RnMailConfig Build() => _mailConfig;
}
