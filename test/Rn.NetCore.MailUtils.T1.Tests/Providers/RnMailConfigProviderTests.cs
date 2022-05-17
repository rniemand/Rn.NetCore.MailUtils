using Microsoft.Extensions.Configuration;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.MailUtils.Providers;

namespace Rn.NetCore.MailUtils.T1.Tests.Providers;

[TestFixture]
public class RnMailConfigProviderTests
{
  [Test]
  public void RnMailConfigProvider_GivenConfigurationKey_ShouldBe_ExpectedValue() =>
    Assert.That(RnMailConfigProvider.Key, Is.EqualTo("Rn.MailUtils"));

  [Test]
  public void RnMailConfigProvider_GivenConstructed_ShouldTryResolveConfiguration()
  {
    // arrange
    var configuration = Substitute.For<IConfiguration>();
    
    // act
    GetProvider(configuration: configuration);

    // assert
    configuration.Received(1).GetSection(RnMailConfigProvider.Key);
  }

  [Test]
  public void RnMailConfigProvider_GivenConfigurationMissing_ShouldLog()
  {
    // arrange
    var configuration = Substitute.For<IConfiguration>();
    var logger = Substitute.For<ILoggerAdapter<RnMailConfigProvider>>();
    var section = Substitute.For<IConfigurationSection>();

    configuration.GetSection(RnMailConfigProvider.Key).Returns(section);
    section.Value.ReturnsNull();

    // act
    GetProvider(
      configuration: configuration,
      logger: logger);

    // assert
    logger.Received(1).LogError("Unable to find mail configuration at: {key}",
      RnMailConfigProvider.Key);
  }

  [Test]
  public void RnMailConfigProvider_GivenConfigurationFound_ShouldLog()
  {
    // arrange
    var configuration = Substitute.For<IConfiguration>();
    var logger = Substitute.For<ILoggerAdapter<RnMailConfigProvider>>();
    var section = Substitute.For<IConfigurationSection>();

    configuration.GetSection(RnMailConfigProvider.Key).Returns(section);

    // act
    GetProvider(
      configuration: configuration,
      logger: logger);

    // assert
    logger.Received(1).LogInformation("Found mail configuration at: {key}",
      RnMailConfigProvider.Key);
  }

  [Test]
  public void RnMailConfigProvider_GivenConfigurationFound_ShouldReturn_BoundConfig()
  {
    // arrange
    var builder = new ConfigurationBuilder();

    builder.AddInMemoryCollection(new List<KeyValuePair<string, string>>
    {
      new($"{RnMailConfigProvider.Key}:Host", "something")
    });

    // act
    var provider = GetProvider(configuration: builder.Build());
    var config = provider.GetRnMailConfig();

    // assert
    Assert.That(config.Host, Is.EqualTo("something"));
  }


  // Internal methods
  private RnMailConfigProvider GetProvider(
    ILoggerAdapter<RnMailConfigProvider>? logger = null,
    IConfiguration? configuration = null)
  {
    return new RnMailConfigProvider(
      logger ?? Substitute.For<ILoggerAdapter<RnMailConfigProvider>>(),
      configuration ?? Substitute.For<IConfiguration>());
  }
}
