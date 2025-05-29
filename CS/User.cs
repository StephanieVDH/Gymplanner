using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymplanner.CS
{
    public class User
    {
        public int Id { get; set; } 
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        //public DateOnly? DateOfBirth { get; set; }
        public string Role { get; set; }
        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now; // Default to current date and time

        public User() { }

        public User(string username, string email, string passwordhash)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordhash;
        }

        public class UserPreferences
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string GoalName { get; set; }
            public string FitnessLevel { get; set; }
            public int AvailableDaysPerWeek { get; set; }
            public int SessionDurationMinutes { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public class UserProfile
        {
            public User User { get; set; }
            public UserPreferences Preferences { get; set; }
        }


    }
}


