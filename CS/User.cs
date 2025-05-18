using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymplanner.CS
{
    public class User
    {
        public int ID { get; set; } // Moet hier ID? Hoe auto incrementen?
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        //public DateOnly? DateOfBirth { get; set; }

        public User() { }

        public User(string username, string email, string passwordhash)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordhash;
            //DateOfBirth = dob;
        }
    }
}
