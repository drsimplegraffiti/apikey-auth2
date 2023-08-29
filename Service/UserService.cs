
using System.Security.Cryptography;
using AuthKeyApp.Data;
using AuthKeyApp.Interfaces;
using AuthKeyApp.Models;

namespace AuthKeyApp.Service
{
    public class UserService : IUserService
    {
         private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> CreateUserAsync(string username)
        {
            var user = new User { Username = username };
            _dbContext.Users.Add(user);

            var apiKey = GenerateApiKey();
            apiKey.User = user;
            _dbContext.ApiKeys.Add(apiKey);

            await _dbContext.SaveChangesAsync();

            return user;
        }

        public User GetUserProfileAsync(int id)
        {
            return _dbContext.Users.SingleOrDefault(u => u.Id == id);
        }

        private ApiKey GenerateApiKey()
        {
            using var cryptoProvider = RandomNumberGenerator.Create();
            var apiKeyBytes = new byte[64];
            cryptoProvider.GetBytes(apiKeyBytes);

            var publicKey = Convert.ToBase64String(apiKeyBytes);

            var privateKeyBytes = new byte[64];
            cryptoProvider.GetBytes(privateKeyBytes);
            var privateKey = Convert.ToBase64String(privateKeyBytes);

            return new ApiKey { PublicKey = publicKey, PrivateKey = privateKey };
        }

    }
}