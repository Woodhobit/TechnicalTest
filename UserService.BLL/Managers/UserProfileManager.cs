using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserService.BLL.Contracts;
using UserService.DAL.Models;
using UserService.DAL.Repository;
using UserService.Infrastructure.DTO;

namespace UserService.BLL.Managers
{
    public class UserProfileManager : IUserProfileManager
    {
        private readonly IRepository<UserProfile> repository;
        private readonly IUserEventHub userEventHub;

        public UserProfileManager(IRepository<UserProfile> repository, IUserEventHub userEventHub)
        {
            this.repository = repository;
            this.userEventHub = userEventHub;
        }

        public async Task UpdateAsync(UserUpdateDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException();
            }

            var entityDb = await this.repository.GetAsync(dto.Id);

            if (entityDb == null)
            {
                throw new ArgumentNullException();
            }

            entityDb.FirstName = dto.FirstName;
            entityDb.SecondName = dto.SecondName;
            entityDb.Email = dto.Email;

            await this.repository.AddOrUpdateAsync(entityDb);

            await this.userEventHub.PublisUserUpdatedEventAsync(entityDb.Id, entityDb.Email);
        }

        public async Task<long> CreateAsync(UserCreateDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException();
            }

            var entity = new UserProfile 
            { 
                FirstName = dto.FirstName, 
                SecondName = dto.SecondName, 
                Email = dto.Email 
            };

            var result = await this.repository.AddOrUpdateAsync(entity);

            await this.userEventHub.PublishUserCreatedEventAsync(result.Id, result.Email);

            return result.Id;
        }

        public async Task<UserProfileDTO> GetByIdAsync(long id)
        {
            return await this.repository
                .Where(x => x.Id == id)
                .Select(x => new UserProfileDTO { Id = x.Id, Name = $"{x.FirstName} {x.SecondName}", Email = x.Email })
                .FirstOrDefaultAsync();
        }
    }
}
