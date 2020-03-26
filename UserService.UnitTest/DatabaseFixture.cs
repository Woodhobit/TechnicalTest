using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using UserService.DAL.Context;
using UserService.DAL.Models;

namespace UserService.UnitTest
{
    public class DatabaseFixture : IDisposable
    {
        public ApplicationContext AppContext { get; private set; }

        public DatabaseFixture()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            this.AppContext = new ApplicationContext(dbContextOptions);

            SeedContext();
        }

        private void SeedContext()
        {
            this.AppContext.UserProfiles.AddRange(this.GetProfiles());
            this.AppContext.SaveChanges();
        }

        public void Dispose()
        {
            this.AppContext.Dispose();
        }

        private new List<UserProfile> GetProfiles()
        {
            return new List<UserProfile>
            {
                new UserProfile
                {
                    Id = 1,
                    Email = "test1@test.com",
                    FirstName = "Test",
                    SecondName = "User 1"
                },
                new UserProfile
                {
                    Id = 2,
                    Email = "test2@test.com",
                    FirstName = "Test",
                    SecondName = "User 2"
                }
            };
        }
    }
}
