using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthKeyApp.Models.Dtos
{
    public class UserProfileDto
    {
        public string? Username { get; set; }
        public List<string> ApiKeys { get; set; } = new List<string>();
    }
}