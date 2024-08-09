using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETCOREWEBAPICRUD.Context;
using ASP.NETCOREWEBAPICRUD.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

public class TaskAPIControllerTests
{
    private readonly Mock<UsersDbContext> _mockContext;
    private readonly Mock<DbSet<Taskss>> _mockTaskSet;
    private readonly Mock<ILogger<TaskAPIController>> _mockLogger;
    private readonly TaskAPIController _controller;

    public TaskAPIControllerTests()
    {
        _mockContext = new Mock<UsersDbContext>(new DbContextOptions<UsersDbContext>());
        _mockTaskSet = new Mock<DbSet<Taskss>>();
        _mockLogger = new Mock<ILogger<TaskAPIController>>();

        _mockContext.Setup(m => m.Tasks).Returns(_mockTaskSet.Object);

        _controller = new TaskAPIController(_mockContext.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetTasks_ReturnsOkResult_WithListOfTasks()
    {
        // Arrange
        var tasks = new List<Taskss>
        {
            new Taskss { TaskId = 1, Title = "Initial Task 1" },
            new Taskss { TaskId = 2, Title = "Initial Task 2" }
        }.AsQueryable();

        _mockTaskSet.As<IQueryable<Taskss>>().Setup(m => m.Provider).Returns(tasks.Provider);
        _mockTaskSet.As<IQueryable<Taskss>>().Setup(m => m.Expression).Returns(tasks.Expression);
        _mockTaskSet.As<IQueryable<Taskss>>().Setup(m => m.ElementType).Returns(tasks.ElementType);
        _mockTaskSet.As<IQueryable<Taskss>>().Setup(m => m.GetEnumerator()).Returns(tasks.GetEnumerator());

        // Act
        var result = await _controller.GetTasks();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnTasks = Assert.IsType<List<Taskss>>(okResult.Value);
        Assert.Equal(2, returnTasks.Count);
    }

    [Fact]
    public async Task GetUsersbyname_ReturnsNotFound_WhenTaskNotExists()
    {
        // Arrange
        int taskId = 1;
        _mockTaskSet.Setup(m => m.FindAsync(taskId)).ReturnsAsync((Taskss)null);

        // Act
        var result = await _controller.GetUsersbyname(taskId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetUsersbyname_ReturnsOkResult_WithTask()
    {
        // Arrange
        int taskId = 1;
        var task = new Taskss { TaskId = taskId, Title = "Initial Task 1" };
        _mockTaskSet.Setup(m => m.FindAsync(taskId)).ReturnsAsync(task);

        // Act
        var result = await _controller.GetUsersbyname(taskId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnTask = Assert.IsType<Taskss>(okResult.Value);
        Assert.Equal(taskId, returnTask.TaskId);
    }

    [Fact]
    public void CreateTask_ReturnsBadRequest_WhenUserOrCategoryIsInvalid()
    {
        // Arrange
        var task = new Taskss { Name = "NonExistentUser", CategoryId = 999 };
        _mockContext.Setup(m => m.User.SingleOrDefault(It.IsAny<Func<Users, bool>>())).Returns((Users)null);
        _mockContext.Setup(m => m.Category.SingleOrDefault(It.IsAny<Func<Categories, bool>>())).Returns((Categories)null);

        // Act
        var result = _controller.CreateTask(task);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("User", badRequestResult.Value.ToString());
        Assert.Contains("Category", badRequestResult.Value.ToString());
    }

    [Fact]
    public async Task Updatetask_ReturnsBadRequest_WhenTaskIdMismatch()
    {
        // Arrange
        int taskId = 1;
        var task = new Taskss { TaskId = 2, Title = "Initial Task 1" };

        // Act
        var result = await _controller.Updatetask(taskId, task);

        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task Updatetask_ReturnsNotFound_WhenTaskNotExists()
    {
        // Arrange
        int taskId = 1;
        var task = new Taskss { TaskId = taskId, Title = "Initial Task 1" };
        _mockTaskSet.Setup(m => m.FindAsync(taskId)).ReturnsAsync((Taskss)null);

        // Act
        var result = await _controller.Updatetask(taskId, task);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Updatetask_ReturnsOkResult_WithUpdatedTask()
    {
        // Arrange
        int taskId = 1;
        var task = new Taskss { TaskId = taskId, Title = "Tnotislask 1" };
        _mockTaskSet.Setup(m => m.FindAsync(taskId)).ReturnsAsync(task);

        var updatedTask = new Taskss { TaskId = taskId, Title = "Updated Task 1" };

        // Act
        var result = await _controller.Updatetask(taskId, updatedTask);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnTask = Assert.IsType<Taskss>(okResult.Value);
        Assert.Equal("Updated Task 1", returnTask.Title);
    }

    [Fact]
    public async Task Deletetask_ReturnsNotFound_WhenTaskNotExists()
    {
        // Arrange
        int taskId = 1;
        _mockTaskSet.Setup(m => m.FindAsync(taskId)).ReturnsAsync((Taskss)null);

        // Act
        var result = await _controller.Deletetask(taskId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Deletetask_ReturnsOkResult_WhenTaskDeleted()
    {
        // Arrange
        int taskId = 1;
        var task = new Taskss { TaskId = taskId, Title = "Task 1" };
        _mockTaskSet.Setup(m => m.FindAsync(taskId)).ReturnsAsync(task);

        // Act
        var result = await _controller.Deletetask(taskId);

        // Assert
        Assert.IsType<OkResult>(result);
    }
}
