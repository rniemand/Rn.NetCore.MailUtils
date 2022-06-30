using System.Text.RegularExpressions;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.MailUtils.Builders;

namespace Rn.NetCore.MailUtils;

public interface IMailTemplateHelper
{
  MailTemplateBuilder GetTemplateBuilder(string templateName);
}

public class MailTemplateHelper : IMailTemplateHelper
{
  private readonly ILoggerAdapter<MailTemplateHelper> _logger;
  private readonly IMailTemplateProvider _templateProvider;
  private readonly RnMailConfig _mailConfig;

  public MailTemplateHelper(
    ILoggerAdapter<MailTemplateHelper> logger,
    IMailTemplateProvider templateProvider,
    RnMailConfig mailConfig)
  {
    _logger = logger;
    _templateProvider = templateProvider;
    _mailConfig = mailConfig;
  }

  public MailTemplateBuilder GetTemplateBuilder(string templateName)
  {
    _logger.LogDebug("Resolving template: {name}", templateName);
    var templateBuilder = new MailTemplateBuilder
    {
      RawTemplate = _templateProvider.GetTemplate(templateName),
      TemplateName = templateName
    };

    if (templateBuilder.TemplateFound)
    {
      ProcessCssTags(templateBuilder);
      InjectGlobalPlaceholders(templateBuilder);
    }

    return templateBuilder;
  }

  private void ProcessCssTags(MailTemplateBuilder builder)
  {
    // (\{css:([^\}]+)\})
    const string regex = "(\\{css:([^\\}]+)\\})";
    if (!builder.RawTemplate.MatchesRegex(regex))
      return;

    var matches = builder.RawTemplate.GetRegexMatches(regex);
    foreach (Match match in matches)
    {
      var rawCss = _templateProvider.GetCss(match.Groups[2].Value);
      builder.RawTemplate = builder.RawTemplate
        .Replace(match.Groups[1].Value, $"<style>{rawCss}</style>");
    }
  }

  private void InjectGlobalPlaceholders(MailTemplateBuilder builder)
  {
    if(_mailConfig.TemplatePlaceholders.Count == 0)
      return;

    foreach (var placeholder in _mailConfig.TemplatePlaceholders)
    {
      builder.AddPlaceHolder($"global.{placeholder.Key}", placeholder.Value);
    }
  }
}
