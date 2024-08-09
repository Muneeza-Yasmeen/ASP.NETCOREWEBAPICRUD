/*using ASP.NETCOREWEBAPICRUD.Context;
using ASP.NETCOREWEBAPICRUD.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class UsersAPIControllerTests
{
    private readonly UsersAPIController _controller;
    private readonly Mock<DbSet<Users>> _usersDbSetMock;
    private readonly Mock<UsersDbContext> _contextMock;

    public UsersAPIControllerTests()
    {
        var users = new List<Users>
        {
            new Users { Name = "John", Email = "2021cs661@student.uet.edu.pk", Address = "New York", Designation = "Developer", Password = "XYZ1Inc" },
            new Users { Name = "Chris", Email = "2021cs662@student.uet.edu.pk", Address = "New York", Designation = "Manager", Password = "ABC2Inc" }
        }.AsQueryable();

        _usersDbSetMock = new Mock<DbSet<Users>>();
        _usersDbSetMock.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(users.Provider);
        _usersDbSetMock.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(users.Expression);
        _usersDbSetMock.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(users.ElementType);
        _usersDbSetMock.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
        _usersDbSetMock.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) => users.SingleOrDefault(u => u.Name == (string)ids[0]));

        _contextMock = new Mock<UsersDbContext>();
        _contextMock.Setup(c => c.User).Returns(_usersDbSetMock.Object);

        _controller = new UsersAPIController(_contextMock.Object);
    }

    [Fact]
    public async Task GetUsers_ReturnsOkResult_WithListOfUsers()
    {
        // Act
        var result = await _controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Users>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetUsersById_ReturnsOkResult_WithUser()
    {
        // Act
        var result = await _controller.GetUsersbyid("John");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Users>(okResult.Value);
        Assert.Equal("John", returnValue.Name);
    }

    [Fact]
    public async Task GetUsersById_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Act
        var result = await _controller.GetUsersbyid("NonExistentUser");

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateUser_ReturnsOkResult_WithUpdatedUser()
    {
        // Arrange
        var updatedUser = new Users { Name = "John", Email = "john_updated@example.com", Address = "updatedaddr", Designation = "Developer", Password = "updatedpass" };

        // Act
        var result = await _controller.Updateuser("John", updatedUser);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Users>(okResult.Value);
        Assert.Equal("john_updated@example.com", returnValue.Email);
    }

    [Fact]
    public async Task UpdateUser_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var user = new Users { Name = "John", Email = "2021cs661@student.uet.edu.pk" };

        // Act
        var result = await _controller.Updateuser("MismatchId", user);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsOkResult()
    {
        // Act
        var result = await _controller.Deleteuser("John");

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsBadRequest_WhenIdIsNull()
    {
        // Act
        var result = await _controller.Deleteuser(null);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}
*/



using ASP.NETCOREWEBAPICRUD.Context;
using ASP.NETCOREWEBAPICRUD.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class UsersAPIControllerTests
{
    private readonly UsersAPIController _controller;
    private readonly Mock<DbSet<Users>> _usersDbSetMock;
    private readonly Mock<UsersDbContext> _contextMock;
    private readonly Mock<ILogger<UsersAPIController>> _loggerMock;

    public UsersAPIControllerTests()
    {
        var users = new List<Users>
        {
            new Users { Name = "John", Email = "2021cs661@student.uet.edu.pk", Address = "New York", Designation = "Developer", Password = "XYZ1Inc" },
            new Users { Name = "Chris", Email = "2021cs662@student.uet.edu.pk", Address = "New York", Designation = "Manager", Password = "ABC2Inc" }
        }.AsQueryable();

        _usersDbSetMock = new Mock<DbSet<Users>>();
        _usersDbSetMock.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(users.Provider);
        _usersDbSetMock.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(users.Expression);
        _usersDbSetMock.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(users.ElementType);
        _usersDbSetMock.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
        _usersDbSetMock.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) => users.SingleOrDefault(u => u.Name == (string)ids[0]));

        _contextMock = new Mock<UsersDbContext>();
        _contextMock.Setup(c => c.User).Returns(_usersDbSetMock.Object);

        _loggerMock = new Mock<ILogger<UsersAPIController>>();

        _controller = new UsersAPIController(_contextMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetUsers_ReturnsOkResult_WithListOfUsers()
    {
        // Act
        var result = await _controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Users>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetUsersById_ReturnsOkResult_WithUser()
    {
        // Act
        var result = await _controller.GetUsersbyid("John");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Users>(okResult.Value);
        Assert.Equal("John", returnValue.Name);
    }

    [Fact]
    public async Task GetUsersById_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Act
        var result = await _controller.GetUsersbyid("NonExistentUser");

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateUser_ReturnsOkResult_WithUpdatedUser()
    {
        // Arrange
        var updatedUser = new Users { Name = "John", Email = "john_updated@example.com", Address = "updatedaddr", Designation = "Developer", Password = "updatedpass" };

        // Act
        var result = await _controller.Updateuser("John", updatedUser);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Users>(okResult.Value);
        Assert.Equal("john_updated@example.com", returnValue.Email);
    }

    [Fact]
    public async Task UpdateUser_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var user = new Users { Name = "John", Email = "2021cs661@student.uet.edu.pk" };

        // Act
        var result = await _controller.Updateuser("MismatchId", user);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsOkResult()
    {
        // Act
        var result = await _controller.Deleteuser("John");

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsBadRequest_WhenIdIsNull()
    {
        // Act
        var result = await _controller.Deleteuser(null);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}
