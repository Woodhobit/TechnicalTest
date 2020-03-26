using System.Threading.Tasks;
using UserService.Infrastructure.DTO;

namespace UserService.BLL.Contracts
{
    public interface IUserEventHub
    {
        public Task PublishUserCreatedEventAsync(long userId, string email);
        public Task PublisUserUpdatedEventAsync(long userId, string email);
    }
}
