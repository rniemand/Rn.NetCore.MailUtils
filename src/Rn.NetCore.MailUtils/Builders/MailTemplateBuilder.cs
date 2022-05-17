namespace Rn.NetCore.MailUtils.Builders;

// DOCS: docs\builders\MailTemplateBuilder.md
public class MailTemplateBuilder
{
  public bool TemplateFound => !string.IsNullOrWhiteSpace(RawTemplate);
  public string RawTemplate { get; set; } = string.Empty;
  public string TemplateName { get; set; } = string.Empty;
  public Dictionary<string, object> Placeholders { get; set; } = new();

  public MailTemplateBuilder AddPlaceHolder(string key, object value)
  {
    Placeholders[key] = value;
    return this;
  }

  public MailTemplateBuilder AddPlaceholders(Dictionary<string, object> placeholders)
  {
    // TODO: [MailTemplateBuilder.AddPlaceholders] (TESTS) Add tests
    foreach (var placeholder in placeholders)
    {
      Placeholders[placeholder.Key] = placeholder.Value;
    }

    return this;
  }

  public string Process()
  {
    var processed = RawTemplate;

    // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
    foreach (var placeholder in Placeholders)
    {
      processed = processed.Replace($"{{{{{placeholder.Key}}}}}",
        GetStringPlaceholder(placeholder.Key));
    }

    return processed;
  }

  private string GetStringPlaceholder(string key)
  {
    // TODO: [MailTemplateBuilder.GetStringPlaceholder] (TESTS) Add tests
    if (!Placeholders.ContainsKey(key))
      return string.Empty;

    var rawValue = Placeholders[key];

    if (rawValue is string strPlaceholder)
      return strPlaceholder;

    if (rawValue is int intValue)
      return intValue.ToString("D");

    if (rawValue is long longValue)
      return longValue.ToString("D");

    if (rawValue is bool boolValue)
      return boolValue ? "true" : "false";

    if (rawValue is DateTime dateValue)
      return dateValue.ToString("s");

    if (rawValue is float floatValue)
      return floatValue.ToString("G");

    if (rawValue is double doubleValue)
      return doubleValue.ToString("G");

    var valueType = rawValue.GetType().Name;
    return $"(UNSUPPORTED:{valueType})";
  }
}
