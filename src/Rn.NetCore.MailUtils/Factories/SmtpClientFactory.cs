using Rn.NetCore.MailUtils.Wrappers;

namespace Rn.NetCore.MailUtils.Factories;

// DOCS: docs\factories\SmtpClientFactory.md
public interface ISmtpClientFactory
{
  ISmtpClient Create();
}

public class SmtpClientFactory : ISmtpClientFactory
{
  public ISmtpClient Create()
  {
    return new SmtpClientWrapper();
  }
}
