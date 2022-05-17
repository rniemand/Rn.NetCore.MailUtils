using Rn.NetCore.MailUtils.Providers;

namespace Rn.NetCore.MailUtils.T1.Tests.Providers;

[TestFixture]
public class RnMailConfigProviderTests
{
  [Test]
  public void RnMailConfigProvider_GivenConfigurationKey_ShouldBe_ExpectedValue() =>
    Assert.That(RnMailConfigProvider.Key, Is.EqualTo("Rn.MailUtils"));
}
