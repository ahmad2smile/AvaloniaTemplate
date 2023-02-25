using Xunit;

namespace AvaloniaTemplate.Tests.XunitConfiguration;

[CollectionDefinition("LiveServerTests collection")]
public class LiverServerTestsCollections : ICollectionFixture<LiveServerTestsFixture>
{
}
