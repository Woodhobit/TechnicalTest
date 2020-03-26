namespace UserService.Infrastructure.DTO
{
    public class UserEvent
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public UserEventType EventType { get; set; }
    }
}
