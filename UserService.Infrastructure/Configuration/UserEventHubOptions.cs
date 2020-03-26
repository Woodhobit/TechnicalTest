namespace UserService.Infrastructure.Configuration
{
    public class UserEventHubOptions
    {
        public string ConnectionString { get; set; }
        public string EventHubName { get; set; }
    }
}
