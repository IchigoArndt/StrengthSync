using StrenghtSync.Infra.SharedKernel;

namespace StrengthSync.Domain.Features.Users
{
    public interface IUserRepository
    {
        public Task<User?> GetUserLoginAsync(string userName);

        public Task<Result<Exception, User>> AddUser(User user);

        public Task<List<string>> GetPermissionsByUserId(long userId);

        public Task<bool> ExisteUserName(string userName);
    }
}