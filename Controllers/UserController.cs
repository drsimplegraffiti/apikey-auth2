using AuthKeyApp.Attributes;
using AuthKeyApp.Data;
using AuthKeyApp.Interfaces;
using AuthKeyApp.Models;
using AuthKeyApp.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AuthKeyApp.Controllers
{

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _dbContext;

        public UserController(IUserService userService, ApplicationDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] CreateUserRequestModel request)
        {
            var user = _userService.CreateUserAsync(request.Username).Result;

            return Ok(new { UserId = user.Id, Message = "User registered successfully" });
        }


        [HttpGet("profile")]
        [ApiKey] // Apply the API key attribute to this action
        public IActionResult GetUserProfile()
        {
            Console.WriteLine("============================");
            // Retrieve the user based on the API key
            var user = GetUserFromApiKey(Request.Headers["Api-Key"]);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userProfile = _userService.GetUserProfileAsync(user.Id);
            var userDetails = new
            {

                UserProfile = userProfile // Assuming userProfile is an actual object with profile data
            };

            return Ok(userDetails);
        }

        private User GetUserFromApiKey(string apiKey)
        {
            if (apiKey == null)
            {
                return null!;
            }
            return _dbContext.Users.SingleOrDefault(u => u.ApiKeys.Any(k => k.PublicKey == apiKey));
        }
    }
}

