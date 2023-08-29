using System;
namespace AuthKeyApp.Models
{
	public class ApiKey
	{
        public int Id { get; set; }
        public string PublicKey { get; set; } = string.Empty;
        public string PrivateKey { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}

