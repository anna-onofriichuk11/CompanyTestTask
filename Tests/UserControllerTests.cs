using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserMicroservice.Controllers;
using UserMicroservice.Entities;
using UserMicroservice.Models.Requests;
using UserMicroservice.Models.Responses;
using UserMicroservice.Services.Interfaces;
using Xunit;

namespace Tests;

public class UserControllerTests
{
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_mapperMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task Create_Returns_OkResult()
    {
        // Arrange
        var request = new CreateUserRequest {UserName = "John", Email = "john@example.com"};
        var user = new User {Name = request.UserName, Email = request.Email};
        _mapperMock.Setup(m => m.Map<User>(request)).Returns(user);

        // Act
        var result = await _controller.Create(request, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<CreateUserResponse>(okResult.Value);
        Assert.Equal(user.Id, response.UserId);

        _userServiceMock.Verify(m => m.AddUserAsync(user, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Get_Returns_OkResult_With_User()
    {
        // Arrange
        var id = 1;
        var user = new User {Id = id, Name = "John", Email = "john@example.com"};
        _userServiceMock.Setup(m => m.GetUserAsync(id, CancellationToken.None)).ReturnsAsync(user);

        // Act
        var result = await _controller.Get(id, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<GetUserResponse>(okResult.Value);
        Assert.Equal(user.Id, response.Id);
        Assert.Equal(user.Name, response.Name);
        Assert.Equal(user.Email, response.Email);
    }

    [Fact]
    public async Task Get_Returns_NotFoundResult_When_User_Is_Null()
    {
        // Arrange
        var id = 1;
        _userServiceMock.Setup(m => m.GetUserAsync(id, CancellationToken.None)).ReturnsAsync(null as User);

        // Act
        var result = await _controller.Get(id, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task SetSubscriptionId_Returns_OkResult()
    {
        // Arrange
        var userId = 1;
        var subscriptionId = 1;

        // Act
        var result = await _controller.SetSubscriptionId(userId, subscriptionId, CancellationToken.None);

        // Assert
        Assert.IsType<OkResult>(result);
        _userServiceMock.Verify(m => m.SetSubscriptionId(userId, subscriptionId, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Put_Returns_OkResult()
    {
        // Arrange
        var id = 1;
        var request = new UpdateUserRequest {UserName = "John", Email = "john@example.com"};
        var user = new User {Id = id, Name = "Jane", Email = "jane@example.com"};
        _userServiceMock.Setup(m => m.GetUserAsync(id, CancellationToken.None)).ReturnsAsync(user);

        // Act
        var result = await _controller.Put(id, request, CancellationToken.None);

        // Assert
        Assert.IsType<OkResult>(result);
        _mapperMock.Verify(m => m.Map(request, user), Times.Once);
        _userServiceMock.Verify(m => m.UpdateUserAsync(user, CancellationToken.None), Times.Once);
    }
}
