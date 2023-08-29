using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthKeyApp.Models;

namespace AuthKeyApp.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string username);
        User GetUserProfileAsync(int id);
    }
}