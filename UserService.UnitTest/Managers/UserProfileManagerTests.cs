using Moq;
using UserService.BLL.Contracts;
using UserService.BLL.Managers;
using UserService.DAL.Models;
using UserService.DAL.Repository;
using UserService.Infrastructure.DTO;
using Xunit;

namespace UserService.UnitTest.Managers
{
    public class UserProfileManagerTests : IClassFixture<DatabaseFixture>
    {
        private readonly IUserProfileManager userProfileManager;

        public UserProfileManagerTests(DatabaseFixture fixture)
        {
            var eventHub = new Mock<IUserEventHub>();
            var userRepository = new Repository<UserProfile>(fixture.AppContext);
            this.userProfileManager = new UserProfileManager(userRepository, eventHub.Object);
        }

        [Fact]
        public async void IsUserCreated()
        {
            // Arrange
            var userProfile = new UserCreateDTO
            {
                Email = "test@test.com",
                FirstName = "Test",
                SecondName = "Name"
            };


            // Act
            var resultId = await this.userProfileManager.CreateAsync(userProfile);

            // Assert
            var createdUser = await this.userProfileManager.GetByIdAsync(resultId);

            Assert.NotNull(createdUser);
            Assert.Equal(userProfile.Email, createdUser.Email);
            Assert.Equal($"{userProfile.FirstName} {userProfile.SecondName}", createdUser.Name);
        }

        [Fact]
        public async void IsUserUpdated()
        {
            // Arrange
            var userProfile = new UserUpdateDTO
            {
                Id = 1,
                Email = "new@test.com",
                FirstName = "New",
                SecondName = "Super Name"
            };

            // Act
            await this.userProfileManager.UpdateAsync(userProfile);

            // Assert
            var updatedProfile = await this.userProfileManager.GetByIdAsync(userProfile.Id);

            Assert.NotNull(updatedProfile);
            Assert.Equal(userProfile.Email, updatedProfile.Email);
            Assert.Equal($"{userProfile.FirstName} {userProfile.SecondName}", updatedProfile.Name);
        }

        [Theory]
        [InlineData(1, "test1@test.com")]
        [InlineData(2, "test2@test.com")]
        public async void GetUserProfileTest(long userId, string email)
        {
            // Act
            var profile = await this.userProfileManager.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(profile);
            Assert.Equal(userId, profile.Id);
            Assert.Equal(email, profile.Email);
        }
    }
}
