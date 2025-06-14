namespace StrengthSync.Domain.Features.Users
{
    public interface IUserRepository
    {
        public User GetUserLogin(string email, string password);

        public void AddUser(User user);

        public List<string> GetPermissionsByUserId(long userId);
    }
}
