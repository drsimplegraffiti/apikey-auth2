using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace AuthKeyApp.Models
{
	public class User
	{
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();
    }
}

