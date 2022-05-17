using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Logging;

namespace Rn.NetCore.MailUtils.Providers;

// DOCS: docs\providers\MailTemplateProvider.md
public interface IMailTemplateProvider
{
  string GetTemplate(string name);
  string GetCss(string name);
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
  private readonly string _cssDir;

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

    _templateDir = GenerateTemplateDirPath();
    _cssDir = GenerateCssDirPath();

    // Will create full pathing
    EnsureDirectoryExists(_cssDir);
  }

  public string GetTemplate(string name)
  {
    // TODO: [MailTemplateProvider.GetTemplate] (TESTS) Add tests
    var tplFilePath = GenerateTemplatePath(name);
    if (!_file.Exists(tplFilePath))
    {
      _logger.LogError("Unable to resolve template file path: {path}", tplFilePath);
      return string.Empty;
    }

    return _file.ReadAllText(tplFilePath);
  }

  public string GetCss(string name)
  {
    // TODO: [MailTemplateProvider.GetCss] (TESTS) Add tests
    var filePath = GenerateCssPath(name);

    if (!_file.Exists(filePath))
    {
      _logger.LogWarning("Unable to find requested CSS file: {path}", filePath);
      return string.Empty;
    }

    return _file.ReadAllText(filePath);
  }

  private string GenerateTemplateDirPath()
  {
    // TODO: [MailTemplateProvider.GenerateTemplateDirPath] (TESTS) Add tests
    var templateDir = _configProvider.GetRnMailConfig().TemplateDir;

    if (templateDir.StartsWith("./"))
      templateDir = _path.Join(_environment.CurrentDirectory, templateDir[2..]);

    // ReSharper disable once ConvertIfStatementToReturnStatement
    if (!_path.EndsInDirectorySeparator(templateDir))
      return _path.Join(templateDir, _path.DirectorySeparatorChar.ToString());

    return templateDir;
  }

  private string GenerateCssDirPath()
  {
    // TODO: [MailTemplateProvider.GenerateCssDirPath] (TESTS) Add tests
    var basePath = _path.Join(_templateDir, "css");

    // ReSharper disable once ConvertIfStatementToReturnStatement
    if (!_path.EndsInDirectorySeparator(basePath))
      return _path.Join(basePath, _path.DirectorySeparatorChar.ToString());

    return basePath;
  }

  private void EnsureDirectoryExists(string path)
  {
    if(_directory.Exists(path))
      return;

    _directory.CreateDirectory(path);
  }

  private string GenerateTemplatePath(string name) =>
    // TODO: [MailTemplateProvider.GenerateTemplatePath] (TESTS) Add tests
    _path.Join(_templateDir, $"{name}.html");

  private string GenerateCssPath(string name) =>
    // TODO: [MailTemplateProvider.GenerateCssPath] (TESTS) Add tests
    _path.Join(_cssDir, $"{name}.css");
}
