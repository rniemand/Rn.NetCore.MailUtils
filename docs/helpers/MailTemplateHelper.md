[Home](/README.md) / [Helpers](/docs/helpers/README.md) / MailTemplateHelper

# MailTemplateHelper
Utility method to help compile mail templates.

## Expectations
The `MailTemplateHelper` expects that the following services are registered on the `DI Container`.

- `ILoggerAdapter<>` - from [Rn.NetCore.Common](https://www.nuget.org/packages/Rn.NetCore.Common/).
- [IRnMailConfigProvider](/docs//providers/RnMailConfigProvider.md) - instance of **RnMailConfigProvider**.
- `IEnvironmentAbstraction` - from [Rn.NetCore.Common](https://www.nuget.org/packages/Rn.NetCore.Common/).
- `IPathAbstraction` - from [Rn.NetCore.Common](https://www.nuget.org/packages/Rn.NetCore.Common/).
- `IDirectoryAbstraction` - from [Rn.NetCore.Common](https://www.nuget.org/packages/Rn.NetCore.Common/).
- `IFileAbstraction` - from [Rn.NetCore.Common](https://www.nuget.org/packages/Rn.NetCore.Common/).

