using StrengthSync.Domain.Features.Users;

namespace StrengthSync.Infra.Data.Features.Users
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<string> GetPermissionsByUserId(long userId)
        {
            throw new NotImplementedException();
        }

        public User GetUserLogin(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
