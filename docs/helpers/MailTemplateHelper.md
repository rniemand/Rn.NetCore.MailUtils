[Home](/README.md) / [Helpers](/docs/helpers/README.md) / MailTemplateHelper

# MailTemplateHelper
Utility method to help compile mail templates.

## Expectations
The `MailTemplateHelper` expects that the following services are registered on the `DI Container`.

- `ILoggerAdapter<>` - from [Rn.NetCore.Common](https://www.nuget.org/packages/Rn.NetCore.Common/).
- `IMailTemplateProvider` - e.g. instance of [MailTemplateProvider](/docs/providers/MailTemplateProvider.md).

## Methods
The following methods are exposed by the `MailTemplateHelper`.

| Method | Since | Returns | Notes |
| --- | --- | --- | --- |
| `GetTemplateBuilder()` | 6.0.1.101 | [MailTemplateBuilder](/docs/builders/MailTemplateBuilder.md) | Returns a new instance of the `MailTemplateBuilder` for the requested mail template. |
