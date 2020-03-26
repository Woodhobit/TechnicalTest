using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using UserService.BLL.Contracts;
using UserService.Infrastructure.Configuration;
using UserService.Infrastructure.DTO;

namespace UserService.BLL.EventHub
{
    public class UserEventHub : IUserEventHub
    {
        private readonly UserEventHubOptions configuration;

        public UserEventHub(IOptions<UserEventHubOptions> options)
        {
            this.configuration = options.Value;
        }

        public async Task PublishUserCreatedEventAsync(long userId, string email)
        {
            var userCreateEvent = new UserEvent
            {
                Id = userId,
                Email = email,
                EventType = UserEventType.UserCreatedEvent
            };

            await this.PublisUserEventAsync(userCreateEvent);
        }

        public async Task PublisUserUpdatedEventAsync(long userId, string email)
        {
            var userUpdateEvent = new UserEvent { 
                Id = userId,
                Email = email, 
                EventType = UserEventType.UserUpdateEvent };

            await this.PublisUserEventAsync(userUpdateEvent);
        }

        public async Task PublisUserEventAsync(UserEvent userEvent)
        {
            await using (var producerClient = new EventHubProducerClient(this.configuration.ConnectionString))
            {
                var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userEvent));
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
                eventBatch.TryAdd(new EventData(data));

                await producerClient.SendAsync(eventBatch);
            }
        }
    }
}
