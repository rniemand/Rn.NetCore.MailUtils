using System.Net.Mail;
using System.Text;
using Rn.NetCore.MailUtils.Config;

namespace Rn.NetCore.MailUtils.Builders;

// DOCS: docs\builders\MailMessageBuilder.md
public class MailMessageBuilder
{
  private readonly MailMessage _mailMessage;
  private static readonly Encoding DefaultEncoding = Encoding.UTF8;

  public MailMessageBuilder()
  {
    _mailMessage = new MailMessage();
  }

  public MailMessageBuilder WithFrom(string address, string displayName, Encoding encoding)
  {
    // TODO: [MailMessageBuilder.WithFrom] (TESTS) Add tests
    _mailMessage.From = new MailAddress(address, displayName, encoding);
    return this;
  }

  public MailMessageBuilder WithFrom(string address, string displayName) =>
    WithFrom(address, displayName, DefaultEncoding);

  public MailMessageBuilder WithFrom(string address) =>
    WithFrom(address, address);

  public MailMessageBuilder WithFrom(RnMailConfig config)
  {
    // TODO: [MailMessageBuilder.WithFrom] (TESTS) Add tests
    var encoding = config.Encoding ?? DefaultEncoding;

    // ReSharper disable once ConvertIfStatementToReturnStatement
    if (!string.IsNullOrWhiteSpace(config.FromName))
    {
      return WithFrom(config.FromAddress, config.FromName, encoding);
    }

    return WithFrom(config.FromAddress, config.FromAddress, encoding);
  }

  public MailMessageBuilder WithTo(string address, string displayName, Encoding encoding)
  {
    // TODO: [MailMessageBuilder.WithTo] (TESTS) Add tests
    _mailMessage.To.Add(new MailAddress(address, displayName, encoding));
    return this;
  }

  public MailMessageBuilder WithTo(string address, string displayName) =>
    WithTo(address, displayName, DefaultEncoding);

  public MailMessageBuilder WithTo(string address) =>
    WithTo(address, address);

  public MailMessageBuilder WithSubject(string subject, Encoding encoding)
  {
    // TODO: [MailMessageBuilder.WithSubject] (TESTS) Add tests
    _mailMessage.Subject = subject;
    _mailMessage.SubjectEncoding = encoding;
    return this;
  }

  public MailMessageBuilder WithSubject(string subject) =>
    WithSubject(subject, DefaultEncoding);

  public MailMessageBuilder WithHtmlBody(string html, Encoding encoding)
  {
    // TODO: [MailMessageBuilder.WithHtmlBody] (TESTS) Add tests
    _mailMessage.Body = html;
    _mailMessage.IsBodyHtml = true;
    _mailMessage.BodyEncoding = encoding;
    return this;
  }

  public MailMessageBuilder WithHtmlBody(string html) =>
    WithHtmlBody(html, DefaultEncoding);

  public MailMessage Build() => _mailMessage;
}
