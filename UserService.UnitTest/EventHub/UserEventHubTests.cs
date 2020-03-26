using Moq;
using UserService.BLL.Contracts;
using UserService.BLL.Managers;
using UserService.DAL.Models;
using UserService.DAL.Repository;
using UserService.Infrastructure.DTO;
using Xunit;

namespace UserService.UnitTest.EventHub
{
    public class UserEventHubTests : IClassFixture<DatabaseFixture>
    {
        private readonly Mock<IUserEventHub> eventHubMoq;
        private readonly IUserProfileManager userProfileManager;

        public UserEventHubTests(DatabaseFixture fixture)
        {
           this.eventHubMoq = new Mock<IUserEventHub>();
           var userRepository = new Repository<UserProfile>(fixture.AppContext);
           this.userProfileManager = new UserProfileManager(userRepository, eventHubMoq.Object);
        }

        [Fact]
        public async void UserCreatedEventPublished()
        {
            // Arrange
            var userProfile = new UserCreateDTO
            {
                Email = "test3@test.com",
                FirstName = "Test",
                SecondName = "User 3"
            };

            // Act
            await userProfileManager.CreateAsync(userProfile);

            // Assert
            this.eventHubMoq.Verify(e => e.PublishUserCreatedEventAsync(It.IsAny<long>(), userProfile.Email), Times.Once);
        }

        [Fact]
        public async void UserUpdatedEventPublished()
        {
            // Arrange
            var userProfile = new UserUpdateDTO
            {
                Id = 2,
                Email = "test3@test.com",
                FirstName = "Test",
                SecondName = "User 3"
            };

            // Act
            await userProfileManager.UpdateAsync(userProfile);

            // Assert
            this.eventHubMoq.Verify(e => e.PublisUserUpdatedEventAsync(userProfile.Id, userProfile.Email), Times.Once);
        }
    }
}
