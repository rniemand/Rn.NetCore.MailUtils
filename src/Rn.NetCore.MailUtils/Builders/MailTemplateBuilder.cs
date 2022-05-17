namespace Rn.NetCore.MailUtils.Builders;

// DOCS: docs\builders\MailTemplateBuilder.md
public class MailTemplateBuilder
{
  public bool TemplateFound => !string.IsNullOrWhiteSpace(RawTemplate);
  public string RawTemplate { get; set; } = string.Empty;
}
