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
    public void GetAllImages_givenListOfImage_loadListOfImages()
    {
        var images =  imageService.GetAllImages();
        Assert.AreEqual(images.First().Name, "");
    }

    [Test]
    public void GetAllImages_givenEmptyImages_empty()
    {
        var images = imageService.GetAllImages();
        Assert.AreEqual(images.First().Name, "");
    }
}
