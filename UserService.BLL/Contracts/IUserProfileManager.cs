using System.Threading.Tasks;
using UserService.Infrastructure.DTO;

namespace UserService.BLL.Contracts
{
    public interface IUserProfileManager
    {
        Task<long> CreateAsync(UserCreateDTO dto);
        Task UpdateAsync(UserUpdateDTO dto);
        Task<UserProfileDTO> GetByIdAsync(long id);
    }
}