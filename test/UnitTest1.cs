using backend_api.Services;

namespace test;

public class Tests
{
    // Test for class from the backend api dependency
    readonly ImageService imageService;
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}
