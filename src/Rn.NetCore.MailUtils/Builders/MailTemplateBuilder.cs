using System.Text.RegularExpressions;
using Rn.NetCore.Common.Extensions;

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

    const string regex = "(\\{\\{([^\\}]+)\\}\\})";
    if (!processed.MatchesRegex(regex))
      return processed;

    var matches = processed.GetRegexMatches(regex);
    foreach (Match match in matches)
    {
      var placeholder = match.Groups[1].Value;
      processed = processed.Replace(placeholder, ResolvePlaceholder(placeholder));
    }

    return processed;
  }

  private string ResolvePlaceholder(string placeholder)
  {
    // TODO: [MailTemplateBuilder.ResolvePlaceholder] (TESTS) Add tests
    placeholder = placeholder
      .Replace("{", "")
      .Replace("}", "");

    if (!placeholder.Contains(":"))
      return GetStringPlaceholder(placeholder, string.Empty);

    var parts = placeholder.Split(":");
    var key = parts[0];
    var format = parts[1].Replace("'", "");

    return GetStringPlaceholder(key, format);
  }

  private string GetStringPlaceholder(string key, string args)
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
      return ProcessDate(dateValue, args);

    if (rawValue is float floatValue)
      return floatValue.ToString("G");

    if (rawValue is double doubleValue)
      return doubleValue.ToString("G");

    var valueType = rawValue.GetType().Name;
    return $"(UNSUPPORTED:{valueType})";
  }

  private static string ProcessDate(DateTime date, string args)
  {
    // TODO: [MailTemplateBuilder.ProcessDate] (TESTS) Add tests
    return date.ToString(string.IsNullOrWhiteSpace(args) ? "s" : args);
  }
}
