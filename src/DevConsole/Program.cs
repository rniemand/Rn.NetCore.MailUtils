using DevConsole;
using Microsoft.Extensions.DependencyInjection;
using Rn.NetCore.Common.Logging;

var logger = DevDIContainer.ServiceProvider
  .GetRequiredService<ILoggerAdapter<Program>>();

logger.LogInformation("Hello world!");
