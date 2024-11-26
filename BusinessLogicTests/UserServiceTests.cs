using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DALEF.Models;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO;

namespace BusinessLogicTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserService> _mockUserService;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            // Mock the IUserService interface
            _mockUserService = new Mock<IUserService>();

            // Initialize UserService with a fake connection string for simplicity
            string fakeConnectionString = "FakeConnectionString";
            _userService = new UserService(fakeConnectionString);
        }

        [Test]
        public void GetUserById_UserExists_ReturnsUser()
        {
            // Arrange
            var expectedUser = new User
            {
                User_Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Role = "Admin"
            };

            _mockUserService.Setup(s => s.GetUserById(1)).Returns(expectedUser);

            // Act
            var user = _mockUserService.Object.GetUserById(1);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(expectedUser.User_Id, user.User_Id);
            Assert.AreEqual(expectedUser.Name, user.Name);
            Assert.AreEqual(expectedUser.Email, user.Email);
            Assert.AreEqual(expectedUser.Role, user.Role);
        }

        [Test]
        public void AuthenticateUser_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var username = "John Doe";
            var email = "john.doe@example.com";
            var expectedUser = new User
            {
                User_Id = 1,
                Name = username,
                Email = email,
                Role = "Admin"
            };

            _mockUserService.Setup(s => s.AuthenticateUser(username, email)).Returns(expectedUser);

            // Act
            var user = _mockUserService.Object.AuthenticateUser(username, email);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(expectedUser.Name, user.Name);
            Assert.AreEqual(expectedUser.Email, user.Email);
            Assert.AreEqual(expectedUser.Role, user.Role);
        }

        [Test]
        public void RegisterUser_ValidUser_AddsUser()
        {
            // Arrange
            var newUser = new User
            {
                User_Id = 2,
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                Role = "User"
            };

            _mockUserService.Setup(s => s.RegisterUser(newUser));

            // Act
            Assert.DoesNotThrow(() => _mockUserService.Object.RegisterUser(newUser));

            // Verify that the RegisterUser method was called once with the expected user
            _mockUserService.Verify(s => s.RegisterUser(It.Is<User>(u => u.Name == newUser.Name && u.Email == newUser.Email && u.Role == newUser.Role)), Times.Once);
        }

        [Test]
        public void GetAllUsers_ReturnsListOfUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User { User_Id = 1, Name = "John Doe", Email = "john.doe@example.com", Role = "Admin" },
                new User { User_Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", Role = "User" }
            };

            _mockUserService.Setup(s => s.GetAllUsers()).Returns(expectedUsers);

            // Act
            var users = _mockUserService.Object.GetAllUsers();

            // Assert
            Assert.IsNotNull(users);
            Assert.AreEqual(2, users.Count);
            Assert.AreEqual(expectedUsers[0].Name, users[0].Name);
            Assert.AreEqual(expectedUsers[1].Name, users[1].Name);
        }
    }
}
