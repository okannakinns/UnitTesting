using Castle.Core.Logging;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Users.Api.DTOs;
using Users.Api.Logging;
using Users.Api.Models;
using Users.Api.Repositories;

namespace Users.Api.Tests.Unit
{
    public class UserServiceTests
    {
        private readonly UserService _sut;
        private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        private readonly ILoggerAdapter _logger = Substitute.For<ILoggerAdapter>();
        public UserServiceTests()
        {
            _sut = new(_userRepository, _logger);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            _userRepository.GetAllAsync().Returns(Enumerable.Empty<User>().ToList());
            // Act
            var result = await _sut.GetAllAsync();
            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnusers_WhenSomeUsersExist()
        {
            // Arrange
            var testUser = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Okan Akın",
            };
            var expectedUsers = new List<User>()
            {
                testUser
            };

            _userRepository.GetAllAsync().Returns(expectedUsers);

            // Act
            var result = await _sut.GetAllAsync();
            // Assert
            result.Should().BeEquivalentTo(expectedUsers);
        }

        [Fact]
        public async Task GetAllAsync_ShouldLogMessages_WhenInvoked()
        {
            // Arrange
            _userRepository.GetAllAsync().Returns(Enumerable.Empty<User>().ToList());
            // Act
            var result = await _sut.GetAllAsync();
            // Assert
            _logger.Received(1).LogInformation(Arg.Is("Retrieving all users"));
            _logger.Received(1).LogInformation(Arg.Is("All users retrieved in {0}ms"), Arg.Any<long>());
        }

        [Fact]
        public async Task GetAllAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
        {
            // Arrange
            var exception = new ArgumentException("Something went wrong while retrieving all users");
            _userRepository.GetAllAsync().Throws(exception);

            // Act
            var requestAction = async () => await _sut.GetAllAsync();

            // Assert
            await requestAction.Should().ThrowAsync<ArgumentException>();

            _logger.Received(1).LogError(Arg.Is(exception), Arg.Is("Something went wrong while retrieving all users"));

        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNoUsersExist()
        {
            // Arrange
            _userRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();
            // Act
            var result = await _sut.GetByIdAsync(Guid.NewGuid());
            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenSomeUserExists()
        {
            // Arrange
            var existingUser = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Okan Akın",
            };

            _userRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(existingUser);

            // Act
            var result = await _sut.GetByIdAsync(Guid.NewGuid());

            // Assert
            result.Should().BeEquivalentTo(existingUser);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldLogMessages_WhenInvoked()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepository.GetByIdAsync(userId).ReturnsNull();
            // Act
            var result = await _sut.GetByIdAsync(userId);
            // Assert
            _logger.Received(1).LogInformation(Arg.Is("Retrieving user with id: {0}"), userId);
            _logger.Received(1).LogInformation(Arg.Is("User with id: {0} retrieved in {1}ms"), userId, Arg.Any<long>());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldLogMessageAndException_WhenExceptionIsThrown()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var exception = new ArgumentException("Something went wrong while retrieving user");
            _userRepository.GetByIdAsync(userId).Throws(exception);

            // Act
            var requestAction = async () => await _sut.GetByIdAsync(userId);

            // Assert
            await requestAction.Should().ThrowAsync<ArgumentException>();

            _logger.Received(1).LogError(Arg.Is(exception), Arg.Is("Something went wrong while retrieving user with id: {0}"), userId);

        }

        [Fact]
        public async Task CreateAsync_ShouldThrownAnError_WhenUserCreateDetailsAreNotValid()
        {
            // Arrange
            CreateUserDto request = new("");

            // Act
            var requestAction = async () => await _sut.CreateAsync(request);

            // Assert
            await requestAction.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task CreateAsync_ShouldThrownAnError_WhenUserNameExist()
        {
            // Arrange
            _userRepository.NameIsExist(Arg.Any<string>()).Returns(true);

            // Act
            var requestAction = async () => await _sut.CreateAsync(new("Okan Akın"));

            // Assert
            await requestAction.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateUserDtoToObject()
        {
            // Arrange
            CreateUserDto request = new("Okan Akın");

            // Act
            var user = _sut.CreateUserDtoToUserObject(request);

            // Assert
            user.FullName.Should().Be(request.FullName);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateUser_WhenDetailsAreValidAndUnique()
        {
            // Arrange
            CreateUserDto request = new("Okan Akın");
            _userRepository.NameIsExist(request.FullName).Returns(false);
            _userRepository.CreateAsync(Arg.Any<User>()).Returns(true);
            // Act
            var user = await _sut.CreateAsync(request);

            // Assert
            user.Should().Be(true);
        }

        [Fact]
        public async Task CreateAsync_ShouldLogMessages_WhenInvoked()
        {
            // Arrange
            CreateUserDto request = new("Okan Akın");
            _userRepository.NameIsExist(request.FullName).Returns(false);
            _userRepository.CreateAsync(Arg.Any<User>()).Returns(true);
            // Act
            var user = await _sut.CreateAsync(request);

            // Assert
            _logger.Received(1).LogInformation(Arg.Is("Creating user with id {0} and full name {1}"),
              Arg.Any<Guid>(),
              request.FullName);

            _logger.Received(1).LogInformation(Arg.Is("User with id: {0} created in {1}ms"),
             Arg.Any<Guid>(),
             Arg.Any<long>());
        }

        [Fact]
        public async Task CreateAsync_ShouldLogMessagesAndExpection_WhenExceptionIsThrown()
        {
            // Arrange
            CreateUserDto request = new("Okan Akın");
            var exception = new ArgumentException("Something went wrong while creating a user");
            _userRepository.CreateAsync(Arg.Any<User>()).Throws(exception);
            // Act
            var requestAction = async () => await _sut.CreateAsync(request);

            // Assert
            await requestAction.Should().ThrowAsync<ArgumentException>();
            _logger.Received(1).LogError(Arg.Is(exception), Arg.Is("Something went wrong while creating a user"));
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldDeleteUser_WhenUserExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var existingUser = new User
            {
                Id = userId,
                FullName = "Okan Akın",
            };
            _userRepository.GetByIdAsync(userId).Returns(existingUser);
            _userRepository.DeleteAsync(existingUser).Returns(true);

            // Act
            var result = await _sut.DeleteByIdAsync(userId);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldThrownAnError_WhenUserNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepository.GetByIdAsync(userId).ReturnsNull();

            // Act
            var action = async () => await _sut.DeleteByIdAsync(userId);

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldLogMessages_WhenInvoked()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                FullName = "Okan Akın",
            };
            _userRepository.GetByIdAsync(userId).Returns(user);
            _userRepository.DeleteAsync(user).Returns(true);

            // Act
            await _sut.DeleteByIdAsync(userId);

            // Assert
            _logger.Received(1).LogInformation(Arg.Is("Deleting user with id {0}"),
              Arg.Any<Guid>());

            _logger.Received(1).LogInformation(Arg.Is("User with id: {0} deleted in {1}ms"),
             Arg.Any<Guid>(),
             Arg.Any<long>());
        }

        [Fact]
        public async Task DeleteAsync_ShouldLogMessagesAndExpection_WhenExceptionIsThrown()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                FullName = "Okan Akın",
            };
            _userRepository.GetByIdAsync(userId).Returns(user);
            var exception = new ArgumentException("Something went wrong while deleting user");
            _userRepository.DeleteAsync(user).Throws(exception);
            // Act
            var requestAction = async () => await _sut.DeleteByIdAsync(userId);

            // Assert
            await requestAction.Should().ThrowAsync<ArgumentException>();
            _logger.Received(1).LogError(Arg.Is(exception), Arg.Is("Something went wrong while deleting a user"));
        }
    }
}
