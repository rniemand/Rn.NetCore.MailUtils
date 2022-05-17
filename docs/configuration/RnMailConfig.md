[Home](/README.md) / [Configuration](/docs/configuration/README.md) / RnMailConfig

# RnMailConfig
Main mail configuration object, provided by [RnMailConfigProvider](/docs/providers/RnMailConfigProvider.md).

> **Note**: The `RnMailConfigProvider` will look at **Rn.MailUtils** for it's configuration.

```json
{
  "Rn.MailUtils": {
    "host": "smtp.gmail.com",
    "port": 587,
    "username": "myuser",
    "password": "mypass",
    "fromAddress": "1@2.com",
    "fromName": "Me",
    "deliveryFormat": "SevenBit",
    "deliveryMethod": "Network",
    "enableSsl": true,
    "timeout": 30000
  }
}
```

## Configuration Properties
Below is a brekdown of each configuration value.

| Path | Type | Required | Default | Notes |
| --- | --- | --- | --- | --- |
| `host` | `string` | optional | smtp.gmail.com | Host to use when connecting to your mail service. |
| `port` | `int` | optional | `587` | Port to use when connecting to your mail service. |
| `username` | `string` | optional | - | Username to use when connecting to your mail service. |
| `password` | `string` | optional | - | Password to use when connecting to your mail service. |
| `fromAddress` | `EMail` | required | - | The from address to use when sending emails. |
| `fromName` | `string` | optional | - | The from name to use when sending emails, defaults to `fromAddress`. |
| `deliveryFormat` | [SmtpDeliveryFormat](https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.smtpdeliveryformat?view=net-6.0) | optional | `SevenBit` | Delivery format to use. |
| `deliveryMethod` | [SmtpDeliveryMethod](https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.smtpdeliverymethod?view=net-6.0) | optional | `Network` | Delivery method to use when sending emails. |
| `enableSsl` | `bool` | optional | `true` | Enabled the usage of SSL. |
| `timeout` | `int` | optional | `30000` | Timeout to use when sending emails. |
