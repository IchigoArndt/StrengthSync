using StrengthSync.Domain.Base;

namespace StrengthSync.Domain.Features.Users
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; private set; } = false;
        public bool IsInstructor { get; set; }
        public bool IsGymStudent { get; set; }
    }
}
