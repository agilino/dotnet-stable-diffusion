using backend_api.Controllers;
using backend_api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class ImageGenerationControllerTests
{
    private readonly Mock<IImageGenerationService> _imageGenerationServiceMock;
    private readonly ImageGenerationController _controller;

    public ImageGenerationControllerTests()
    {
        _imageGenerationServiceMock = new Mock<IImageGenerationService>();
        _controller = new ImageGenerationController(_imageGenerationServiceMock.Object);
    }

    [Fact]
    public async Task GeneratePNGImage_GivenCorrectPrompt_ReturnsPNGImage()
    {
        // Arrange
        string prompt = "Test Prompt";
        var expectedImageResult = new PhysicalFileResult("filePath", "image/png") { FileDownloadName = "fileName" };

        _imageGenerationServiceMock
            .Setup(x => x.GenerateImage(prompt))
            .ReturnsAsync(expectedImageResult);

        // Act
        var result = await _controller.GeneratePNGImage(prompt);

        // Assert
        Assert.Equal(expectedImageResult, result);
    }

    [Fact]
    public async Task GeneratePNGImage_GivenEmptyPrompt_ReturnsBadRequest()
    {
        //Arrange
        string emptyString = string.Empty;
        var expectedBadRequest = new BadRequestObjectResult("Prompt cannot be empty!");

        _imageGenerationServiceMock
            .Setup(x => x.GenerateImage(emptyString))
            .ReturnsAsync(expectedBadRequest);

        //Act
        var result = await _controller.GeneratePNGImage(emptyString);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errorMessage = Assert.IsAssignableFrom<string>(badRequestResult.Value);

        Assert.Equal(expectedBadRequest.StatusCode, badRequestResult.StatusCode);
        Assert.Equal(expectedBadRequest.Value, badRequestResult.Value);
    }

    [Fact]
    public void GetGalleryImages_GetAllImageUrls_ReturnsAllImageUrls()
    {
        // Arrange
        var imageUrls = new List<string> { "url1", "url2", "url3" };
        _imageGenerationServiceMock.Setup(x => x.GetAllImages()).Returns(imageUrls);

        // Act
        var result = _controller.GetGalleryImages();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualImageUrls = Assert.IsAssignableFrom<List<string>>(okResult.Value);

        Assert.Equal(imageUrls.Count, actualImageUrls.Count);
        for (int i = 0; i < imageUrls.Count; i++)
        {
            Assert.Equal(imageUrls[i], actualImageUrls[i]);
        }
    }

    [Fact]
    public void GetImage_GivenCorrectImageName_ReturnsImage()
    {
        // Arrange
        string imageName = "example.png";

        // Act
        var result = _controller.GetImage(imageName);

        // Assert
        var fileResult = Assert.IsType<FileContentResult>(result);
        var imageBytes = fileResult.FileContents;

        Assert.NotNull(imageBytes);
    }

    [Fact]
    public void GetImage_GivenEmptyImageName_ReturnsBadRequest()
    {
        // Arrange
        string emptyImageName = string.Empty;

        // Act
        var result = _controller.GetImage(emptyImageName);

        // Assert
        Assert.IsType<FileContentResult>(result);

        var fileResult = Assert.IsType<FileContentResult>(result);
        var imageBytes = fileResult.FileContents;

        Assert.Empty(imageBytes);
    }
}
