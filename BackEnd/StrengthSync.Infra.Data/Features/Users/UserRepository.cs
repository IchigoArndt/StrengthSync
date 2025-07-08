using Microsoft.EntityFrameworkCore;
using StrenghtSync.Infra.SharedKernel;
using StrengthSync.Domain.Features.Users;
using StrengthSync.Infra.Data.Contexts;

namespace StrengthSync.Infra.Data.Features.Users
{
    public class UserRepository (StrengthSyncDbContext context) : IUserRepository
    {
        public async Task<Result<Exception, User>> AddUser(User user)
        {
            context.Users.Add(user);

            try
            {
                user.Id = await context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex) 
            {
                return ex;
            }

        }

        public async Task<bool> ExisteUserName(string userName)
        {
            var user = await context.Users.Where(x => x.Username.ToUpper().Equals(userName.ToUpper())).FirstOrDefaultAsync();

            return user != null ? true : false;
        }

        public async Task<List<string>> GetPermissionsByUserId(long userId)
        {
            List<string> permissions = new List<string>();

            var user = await context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (user == null)
                return new List<string>();

            if (user.IsAdmin)
                permissions.Add("Administrador");

            if (user.IsInstructor)
                permissions.Add("Instructor");

            if (user.IsGymStudent)
                permissions.Add("Student");

            return permissions;
        }

        public async Task<User?> GetUserLoginAsync(string userName)
        {
            var user = await context.Users.Where(user => user.Username.ToUpper().Equals(userName.ToUpper())).FirstOrDefaultAsync();

            return user;
        }
    }
}
