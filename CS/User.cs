using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymplanner.CS
{
    public class User
    {
        public int Id { get; set; } // Moet hier ID? Hoe auto incrementen?
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        //public DateOnly? DateOfBirth { get; set; }
        public string Role { get; set; } = "user"; // Default role is 'user'

        public DateTime CreatedAt { get; set; } = DateTime.Now; // Default to current date and time

        public User() { }

        public User(string username, string email, string passwordhash)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordhash;
        }
    }
}
