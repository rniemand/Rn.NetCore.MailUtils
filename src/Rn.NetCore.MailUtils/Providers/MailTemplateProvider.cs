using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Logging;

namespace Rn.NetCore.MailUtils.Providers;

// DOCS: docs\providers\MailTemplateProvider.md
public interface IMailTemplateProvider
{
  string GetTemplate(string name);
}

public class MailTemplateProvider : IMailTemplateProvider
{
  private readonly ILoggerAdapter<MailTemplateProvider> _logger;
  private readonly IRnMailConfigProvider _configProvider;
  private readonly IEnvironmentAbstraction _environment;
  private readonly IPathAbstraction _path;
  private readonly IDirectoryAbstraction _directory;
  private readonly IFileAbstraction _file;
  private readonly string _templateDir;

  public MailTemplateProvider(
    ILoggerAdapter<MailTemplateProvider> logger,
    IRnMailConfigProvider configProvider,
    IEnvironmentAbstraction environment,
    IPathAbstraction path,
    IDirectoryAbstraction directory,
    IFileAbstraction file)
  {
    _logger = logger;
    _configProvider = configProvider;
    _environment = environment;
    _path = path;
    _directory = directory;
    _file = file;

    _templateDir = GenerateTemplateDir();
  }

  public string GetTemplate(string name)
  {
    // TODO: [MailTemplateProvider.GetTemplate] (TESTS) Add tests
    Console.WriteLine();
    return string.Empty;
  }

  private string GenerateTemplateDir()
  {
    // TODO: [MailTemplateProvider.GenerateTemplateDir] (TESTS) Add tests
    var templateDir = _configProvider.GetRnMailConfig().TemplateDir;

    if (templateDir.StartsWith("./"))
      templateDir = _path.Join(_environment.CurrentDirectory, templateDir[2..]);

    // ReSharper disable once ConvertIfStatementToReturnStatement
    if (!_path.EndsInDirectorySeparator(templateDir))
      return _path.Join(templateDir, _path.DirectorySeparatorChar.ToString());

    return templateDir;
  }
}
