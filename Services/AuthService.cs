using DDServices.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DDServices.Services
{
    public class AuthService
    {
        private readonly IMongoCollection<User> _user;

        public AuthService(IOptions<MongoDBSettings> options)
        {
            var mongoDBClient = new MongoClient(options.Value.ConnectionURI);

            _user = mongoDBClient.GetDatabase(options.Value.DatabaseName).GetCollection<User>(options.Value.UserCollectionName);

        }

        public async Task<User> LoginUser(Login login)
        {
            var user = await _user.Find(m => m.email == login.email && m.isActive == true).FirstOrDefaultAsync();

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(login.password, user.password))
                {
                    return user;
                }
            }
            return null;
        }

        public async Task CreateUser(User newUser)
        {
            newUser.userID = Guid.NewGuid().ToString("N");
            newUser.password = HashPassword(newUser.password);
            await _user.InsertOneAsync(newUser);
        }

        private string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

    }
}
