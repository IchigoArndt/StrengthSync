using StrengthSync.Domain.Base;

namespace StrengthSync.Domain.Features.Users
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
