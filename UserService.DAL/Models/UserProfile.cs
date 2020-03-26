namespace UserService.DAL.Models
{
    public class UserProfile : BaseEntity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
    }
}
