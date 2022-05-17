using System.Text.RegularExpressions;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.MailUtils.Builders;
using Rn.NetCore.MailUtils.Providers;

namespace Rn.NetCore.MailUtils.Helpers;

public interface IMailTemplateHelper
{
  MailTemplateBuilder GetTemplateBuilder(string templateName);
}

public class MailTemplateHelper : IMailTemplateHelper
{
  private readonly ILoggerAdapter<MailTemplateHelper> _logger;
  private readonly IMailTemplateProvider _templateProvider;

  public MailTemplateHelper(
    ILoggerAdapter<MailTemplateHelper> logger,
    IMailTemplateProvider templateProvider)
  {
    _logger = logger;
    _templateProvider = templateProvider;
  }

  public MailTemplateBuilder GetTemplateBuilder(string templateName)
  {
    // TODO: [MailTemplateHelper.GetTemplateBuilder] (TESTS) Add tests
    _logger.LogDebug("Resolving template: {name}", templateName);
    var templateBuilder = new MailTemplateBuilder
    {
      RawTemplate = _templateProvider.GetTemplate(templateName),
      TemplateName = templateName
    };

    if (templateBuilder.TemplateFound)
    {
      ProcessCssTags(templateBuilder);
    }

    return templateBuilder;
  }

  private void ProcessCssTags(MailTemplateBuilder builder)
  {
    // TODO: [MailTemplateHelper.ProcessCssTags] (TESTS) Add tests
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
}
