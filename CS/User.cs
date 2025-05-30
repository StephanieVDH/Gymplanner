using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gymplanner.CS.User;

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
        public int RoleId { get; set; }
        public string Picture { get; set; }
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
        public class UserProfile : INotifyPropertyChanged
        {
            public User User { get; set; }
            public UserPreferences Preferences { get; set; }

            private string _picture;
            public string Picture
            {
                get => _picture;
                set
                {
                    if (_picture == value) return;
                    _picture = value;
                    if (User != null)
                    User.Picture = value;
                    OnPropertyChanged(nameof(Picture));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}


