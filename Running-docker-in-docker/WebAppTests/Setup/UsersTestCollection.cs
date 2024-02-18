using Xunit;

namespace WebAppTests.Setup;

[CollectionDefinition(nameof(UsersTestCollection))]
public class UsersTestCollection : ICollectionFixture<AppFixture>
{
    
}