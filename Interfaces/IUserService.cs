
using AuthKeyApp.Models;

namespace AuthKeyApp.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string username);
        User GetUserProfileAsync(int id);
    }
}