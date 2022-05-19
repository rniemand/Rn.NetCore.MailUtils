[Home](/README.md) / [Misc](/docs/misc/README.md) / Service Registration

# Service Registration
This page covers what services are registered when you call the `.AddRnMailUtils()` method.

## Registers
The `.AddRnMailUtils()` extension method will register the following obhects on the provided `IServiceCollection` instance.

| Interface | Reg. Method | Notes |
| --- | --- | --- |
| `IEnvironmentAbstraction` | `TryAddSingleton()` | - |
| `IPathAbstraction` | `TryAddSingleton()` | - |
| `IDirectoryAbstraction` | `TryAddSingleton()` | - |
| `IFileAbstraction` | `TryAddSingleton()` | - |
| `ILoggerAdapter<>` | `TryAddSingleton()` | - |
| `ISmtpClientFactory` | `AddSingleton()` | - |
| `IMailMessageBuilderFactory` | `AddSingleton()` | - |
| `IRnMailConfigProvider` | `AddSingleton()` | - |
| `IMailTemplateProvider` | `AddSingleton()` | - |
| `IMailTemplateHelper` | `AddSingleton()` | - |

## Depends On
The following objects are not registered on the `IServiceCollection` instance, but are expected to be present.

- `IConfiguration` - used to resolve configuration for the mailer
