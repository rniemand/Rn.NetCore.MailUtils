using System.Net.Mail;

namespace Rn.NetCore.MailUtils.Builders;

// DOCS: docs\builders\MailMessageBuilder.md
public class MailMessageBuilder
{
  private readonly MailMessage _mailMessage;

  public MailMessageBuilder()
  {
    _mailMessage = new MailMessage();
  }

  public MailMessage Build() => _mailMessage;
}
