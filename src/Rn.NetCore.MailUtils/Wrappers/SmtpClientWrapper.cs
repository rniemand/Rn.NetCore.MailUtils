using System.Net;
using System.Net.Mail;

namespace Rn.NetCore.MailUtils.Wrappers;

// DOCS: docs\wrappers\SmtpClientWrapper.md
public interface ISmtpClient
{
  SmtpDeliveryFormat DeliveryFormat { get; set; }
  SmtpDeliveryMethod DeliveryMethod { get; set; }
  bool EnableSsl { get; set; }
  string? PickupDirectoryLocation { get; set; }
  string? TargetName { get; set; }
  int Timeout { get; set; }
  bool UseDefaultCredentials { get; set; }
  ICredentialsByHost? Credentials { get; set; }

  void Send(MailMessage message);
  void Send(string from, string recipients, string? subject, string? body);
  void SendAsync(MailMessage message, object? userToken);
  void SendAsync(string from, string recipients, string? subject, string? body, object? userToken);
  void SendAsyncCancel();
  Task SendMailAsync(MailMessage message);
  Task SendMailAsync(MailMessage message, CancellationToken cancellationToken);
  Task SendMailAsync(string from, string recipients, string? subject, string? body);

  Task SendMailAsync(string from, string recipients, string? subject, string? body, CancellationToken cancellationToken);
}

public class SmtpClientWrapper : ISmtpClient
{
  public SmtpDeliveryFormat DeliveryFormat
  {
    get => _smtpClient.DeliveryFormat;
    set => _smtpClient.DeliveryFormat = value;
  }

  public SmtpDeliveryMethod DeliveryMethod
  {
    get => _smtpClient.DeliveryMethod;
    set => _smtpClient.DeliveryMethod = value;
  }

  public bool EnableSsl
  {
    get => _smtpClient.EnableSsl;
    set => _smtpClient.EnableSsl = value;
  }

  public string? PickupDirectoryLocation
  {
    get => _smtpClient.PickupDirectoryLocation;
    set => _smtpClient.PickupDirectoryLocation = value;
  }

  public string? TargetName
  {
    get => _smtpClient.TargetName;
    set => _smtpClient.TargetName = value;
  }

  public int Timeout
  {
    get => _smtpClient.Timeout;
    set => _smtpClient.Timeout = value;
  }

  public bool UseDefaultCredentials
  {
    get => _smtpClient.UseDefaultCredentials;
    set => _smtpClient.UseDefaultCredentials = value;
  }

  public ICredentialsByHost? Credentials
  {
    get => _smtpClient.Credentials;
    set => _smtpClient.Credentials = value;
  }

  private readonly SmtpClient _smtpClient;


  // Constructors
  public SmtpClientWrapper()
  {
    _smtpClient = new SmtpClient();
  }

  public SmtpClientWrapper(string host)
  {
    _smtpClient = new SmtpClient(host);
  }

  public SmtpClientWrapper(string host, int port)
  {
    _smtpClient = new SmtpClient(host, port);
  }


  // Methods
  public void Send(MailMessage message) =>
    _smtpClient.Send(message);

  public void Send(string from, string recipients, string? subject, string? body) =>
    _smtpClient.Send(from, recipients, subject, body);

  public void SendAsync(MailMessage message, object? userToken) =>
    _smtpClient.SendAsync(message, userToken);

  public void SendAsync(string from, string recipients, string? subject, string? body, object? userToken) =>
    _smtpClient.SendAsync(from, recipients, subject, body, userToken);

  public void SendAsyncCancel() =>
    _smtpClient.SendAsyncCancel();

  public async Task SendMailAsync(MailMessage message) =>
    await _smtpClient.SendMailAsync(message);

  public async Task SendMailAsync(MailMessage message, CancellationToken cancellationToken) =>
    await _smtpClient.SendMailAsync(message, cancellationToken);

  public async Task SendMailAsync(string from, string recipients, string? subject, string? body) =>
    await _smtpClient.SendMailAsync(from, recipients, subject, body);

  public async Task SendMailAsync(string from, string recipients, string? subject, string? body, CancellationToken cancellationToken) =>
    await _smtpClient.SendMailAsync(from, recipients, subject, body, cancellationToken);
}
