using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.MailUtils.Providers;

namespace Rn.NetCore.MailUtils.Helpers;

public interface IMailTemplateHelper
{
}

public class MailTemplateHelper : IMailTemplateHelper
{
  private readonly ILoggerAdapter<MailTemplateHelper> _logger;
  private readonly IRnMailConfigProvider _mailConfigProvider;
  private readonly IEnvironmentAbstraction _environment;
  private readonly IPathAbstraction _path;
  private readonly IDirectoryAbstraction _directory;
  private readonly IFileAbstraction _file;
  private readonly string _templateDirectory;

  public MailTemplateHelper(
    ILoggerAdapter<MailTemplateHelper> logger,
    IRnMailConfigProvider mailConfigProvider,
    IEnvironmentAbstraction environment,
    IPathAbstraction path,
    IDirectoryAbstraction directory,
    IFileAbstraction file)
  {
    _logger = logger;
    _mailConfigProvider = mailConfigProvider;
    _environment = environment;
    _path = path;
    _directory = directory;
    _file = file;

    _templateDirectory = GenerateTemplateDirPath();
    EnsureTemplateDirExists();
  }

  private string GenerateTemplateDirPath()
  {
    // TODO: [MailTemplateHelper.GenerateTemplateDirPath] (TESTS) Add tests
    var templateDir = _mailConfigProvider.GetRnMailConfig().TemplateDir;

    if (templateDir.StartsWith("./"))
      templateDir = _path.Join(_environment.CurrentDirectory, templateDir[2..]);

    // ReSharper disable once ConvertIfStatementToReturnStatement
    if (!_path.EndsInDirectorySeparator(templateDir))
      return _path.Join(templateDir, _path.DirectorySeparatorChar.ToString());

    return templateDir;
  }

  private void EnsureTemplateDirExists()
  {
    // TODO: [MailTemplateHelper.EnsureTemplateDirExists] (TESTS) Add tests
    if(_directory.Exists(_templateDirectory))
      return;

    _directory.CreateDirectory(_templateDirectory);
  }
}
