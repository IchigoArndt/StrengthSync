using MongoDB.Driver;
using StrenghtSync.Infra.SharedKernel;
using StrengthSync.Domain.Features.Users;

namespace StrengthSync.Infra.Data.Features.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<User> _users;

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
            _users = _database.GetCollection<User>("Users");
        }

        public async Task<Result<Exception,long>> AddUser(User user)
        {
            Random rdr = new Random();

            user._id = rdr.Next();

            try
            {
                await _users.InsertOneAsync(user);

                return user._id;

            }catch (Exception ex)
            {
                return new Exception(ex.Message);
            }
            
        }

        public async Task<bool> ExisteUserName(string userName)
        {
            var user = await _users.FindAsync(user => user.Username == userName);

            return user.Any();
        }

        public async Task<List<string>> GetPermissionsByUserId(long userId)
        {
            List<string> permissions = new List<string>();

            var users = await _users.FindAsync(user => user._id == userId);

            if (users == null)
                return new List<string>();

            var user = users.FirstOrDefault();

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
            var user = await _users.FindAsync(user => user.Username == userName);

            return user.FirstOrDefault();
        }
    }
}
