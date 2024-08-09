/*using ASP.NETCOREWEBAPICRUD.Context;
using ASP.NETCOREWEBAPICRUD.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ASP.NETCOREWEBAPICRUD.Tests
{
    public class UsersControllerTests
    {
        private UsersDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
                .UseInMemoryDatabase(databaseName: "UsersDbContext")
                .Options;

            var context = new UsersDbContext(options);

            context.User.Add(new Users { Name = "John", Designation = "Developer", Address = "New York", Password = "XYZ1Inc", Email = "2021cs661@student.uet.edu.pk" });
            context.User.Add(new Users { Name = "Chris", Designation = "Manager", Address = "New York", Password = "ABC2Inc", Email = "2021cs662@student.uet.edu.pk" });
            context.User.Add(new Users { Name = "Mukesh", Designation = "Consultant", Address = "New Delhi", Password = "XYZ3Inc", Email = "2021cs663@student.uet.edu.pk" });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersAPIController(context);

            // Act
            var result = await controller.GetUsers();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var users = Assert.IsAssignableFrom<List<Users>>(actionResult.Value);
            Assert.Equal(3, users.Count);
        }

        [Fact]
        public async Task GetUsersbyid_ReturnsOkResult_WithUser()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersAPIController(context);

            // Act
            var result = await controller.GetUsersbyid("John");

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var user = Assert.IsAssignableFrom<Users>(actionResult.Value);
            Assert.Equal("John", user.Name);
        }

        [Fact]
        public async Task GetUsersbyid_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersAPIController(context);

            // Act
            var result = await controller.GetUsersbyid("NonExistentUser");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Createuser_ReturnsOkResult()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersAPIController(context);
            var newUser = new Users { Name = "NewUser", Designation = "Tester", Address = "Los Angeles", Password = "Test123", Email = "newuser@example.com" };

            // Act
            var result = controller.createuser(newUser);

            // Assert
            Assert.IsType<OkResult>(result);
            var user = context.User.Find("NewUser");
            Assert.NotNull(user);
        }

        [Fact]
        public async Task Updateuser_ReturnsBadRequest_WhenNameMismatch()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersAPIController(context);
            var updatedUser = new Users { Name = "DifferentName", Designation = "Tester", Address = "Los Angeles", Password = "Test123", Email = "newuser@example.com" };

            // Act
            var result = await controller.Updateuser("John", updatedUser);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task Updateuser_ReturnsOkResult_WithUpdatedUser()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersAPIController(context);
            var updatedUser = new Users { Name = "John", Designation = "Senior Developer", Address = "San Francisco", Password = "UpdatedPassword", Email = "updated@example.com" };

            // Act
            var result = await controller.Updateuser("John", updatedUser);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var user = Assert.IsAssignableFrom<Users>(actionResult.Value);
            Assert.Equal("Senior Developer", user.Designation);
            Assert.Equal("San Francisco", user.Address);
        }

        [Fact]
        public async Task Deleteuser_ReturnsOkResult()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersAPIController(context);

            // Act
            var result = await controller.Deleteuser("John");

            // Assert
            Assert.IsType<OkResult>(result);
            var user = context.User.Find("John");
            Assert.Null(user);
        }
    }
}
*/


/*using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP.NETCOREWEBAPICRUD.Context;
using ASP.NETCOREWEBAPICRUD.Controllers;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCOREWEBAPICRUD.Tests
{
    public class UsersControllerTests
    {
        private UsersDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
                .UseInMemoryDatabase(databaseName: "UsersDbContext")
                .Options;

            var context = new UsersDbContext(options);

            context.User.Add(new Users { Name = "John", Designation = "Developer", Address = "New York", Password = "XYZ1Inc", Email = "2021cs661@student.uet.edu.pk" });
            context.User.Add(new Users { Name = "Chris", Designation = "Manager", Address = "New York", Password = "ABC2Inc", Email = "2021cs662@student.uet.edu.pk" });
            context.User.Add(new Users { Name = "Mukesh", Designation = "Consultant", Address = "New Delhi", Password = "XYZ3Inc", Email = "2021cs663@student.uet.edu.pk" });
            context.SaveChanges();

            return context;
        }

        private UsersAPIController GetController(UsersDbContext context)
        {
            var logger = new Mock<ILogger<UsersAPIController>>().Object;
            return new UsersAPIController(context, logger);
        }

        [Fact]
        public async Task GetUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);

            // Act
            var result = await controller.GetUsers();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var users = Assert.IsAssignableFrom<List<Users>>(actionResult.Value);
            Assert.Equal(3, users.Count);
        }

        [Fact]
        public async Task GetUsersbyid_ReturnsOkResult_WithUser()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);

            // Act
            var result = await controller.GetUsersbyid("John");

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var user = Assert.IsAssignableFrom<Users>(actionResult.Value);
            Assert.Equal("John", user.Name);
        }

        [Fact]
        public async Task GetUsersbyid_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);

            // Act
            var result = await controller.GetUsersbyid("NonExistentUser");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Createuser_ReturnsOkResult()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);
            var newUser = new Users { Name = "NewUser", Designation = "Tester", Address = "Los Angeles", Password = "Test123", Email = "newuser@example.com" };

            // Act
            var result = controller.createuser(newUser);

            // Assert
            Assert.IsType<OkResult>(result);
            var user = context.User.SingleOrDefault(u => u.Name == "NewUser");
            Assert.NotNull(user);
        }

        [Fact]
        public async Task Updateuser_ReturnsBadRequest_WhenNameMismatch()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);
            var updatedUser = new Users { Name = "DifferentName", Designation = "Tester", Address = "Los Angeles", Password = "Test123", Email = "newuser@example.com" };

            // Act
            var result = await controller.Updateuser("John", updatedUser);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task Updateuser_ReturnsOkResult_WithUpdatedUser()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);
            var updatedUser = new Users { Name = "John", Designation = "Senior Developer", Address = "San Francisco", Password = "UpdatedPassword", Email = "updated@example.com" };

            // Act
            var result = await controller.Updateuser("John", updatedUser);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var user = Assert.IsAssignableFrom<Users>(actionResult.Value);
            Assert.Equal("Senior Developer", user.Designation);
            Assert.Equal("San Francisco", user.Address);
        }

        [Fact]
        public async Task Deleteuser_ReturnsOkResult()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);

            // Act
            var result = await controller.Deleteuser("John");

            // Assert
            Assert.IsType<OkResult>(result);
            var user = context.User.SingleOrDefault(u => u.Name == "John");
            Assert.Null(user);
        }
    }
}*/



using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP.NETCOREWEBAPICRUD.Context;
using ASP.NETCOREWEBAPICRUD.Controllers;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCOREWEBAPICRUD.Tests
{
    public class UsersControllerTests
    {
        private UsersDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
                .UseInMemoryDatabase(databaseName: "UsersDbContext")
                .Options;

            var context = new UsersDbContext(options);

            context.User.Add(new Users { Name = "John", Designation = "Developer", Address = "New York", Password = "XYZ1Inc", Email = "2021cs661@student.uet.edu.pk" });
            context.User.Add(new Users { Name = "Chris", Designation = "Manager", Address = "New York", Password = "ABC2Inc", Email = "2021cs662@student.uet.edu.pk" });
            context.User.Add(new Users { Name = "Mukesh", Designation = "Consultant", Address = "New Delhi", Password = "XYZ3Inc", Email = "2021cs663@student.uet.edu.pk" });
            context.SaveChanges();

            return context;
        }

        private UsersAPIController GetController(UsersDbContext context)
        {
            var logger = new Mock<ILogger<UsersAPIController>>().Object;
            return new UsersAPIController(context, logger);
        }

        [Fact]
        public async Task GetUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);

            // Act
            var result = await controller.GetUsers();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var users = Assert.IsAssignableFrom<List<Users>>(actionResult.Value);
            Assert.Equal(3, users.Count);
        }

        [Fact]
        public async Task GetUsersbyid_ReturnsOkResult_WithUser()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);

            // Act
            var result = await controller.GetUsersbyid("John");

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var user = Assert.IsAssignableFrom<Users>(actionResult.Value);
            Assert.Equal("John", user.Name);
        }

        [Fact]
        public async Task GetUsersbyid_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);

            // Act
            var result = await controller.GetUsersbyid("NonExistentUser");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Createuser_ReturnsOkResult()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);
            var newUser = new Users { Name = "NewUser", Designation = "Tester", Address = "Los Angeles", Password = "Test123", Email = "newuser@example.com" };

            // Act
            var result = controller.createuser(newUser);

            // Assert
            Assert.IsType<OkResult>(result);
            var user = context.User.SingleOrDefault(u => u.Name == "NewUser");
            Assert.NotNull(user);
        }

        [Fact]
        public async Task Updateuser_ReturnsBadRequest_WhenNameMismatch()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);
            var updatedUser = new Users { Name = "DifferentName", Designation = "Tester", Address = "Los Angeles", Password = "Test123", Email = "newuser@example.com" };

            // Act
            var result = await controller.Updateuser("John", updatedUser);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task Updateuser_ReturnsOkResult_WithUpdatedUser()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);
            var updatedUser = new Users { Name = "John", Designation = "Senior Developer", Address = "San Francisco", Password = "UpdatedPassword", Email = "updated@example.com" };

            // Act
            var result = await controller.Updateuser("John", updatedUser);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var user = Assert.IsAssignableFrom<Users>(actionResult.Value);
            Assert.Equal("Senior Developer", user.Designation);
            Assert.Equal("San Francisco", user.Address);
        }

        [Fact]
        public async Task Deleteuser_ReturnsOkResult()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = GetController(context);

            // Act
            var result = await controller.Deleteuser("John");

            // Assert
            Assert.IsType<OkResult>(result);
            var user = context.User.SingleOrDefault(u => u.Name == "John");
            Assert.Null(user);
        }
    }
}
