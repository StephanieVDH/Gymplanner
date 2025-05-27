using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Gymplanner.CS;
using static Gymplanner.CS.User;

namespace Gymplanner.Windows
{
    public partial class ProfileWindow : Window
    {
        private UserProfile userProfile;

        // Constructor that accepts user data
        public ProfileWindow(User user)
        {
            InitializeComponent();
            LoadUserProfile(user);
        }

        // Keep the default constructor for design-time support
        public ProfileWindow()
        {
            InitializeComponent();
            LoadDefaultData();
        }

        private void LoadUserProfile(User user)
        {
            try
            {
                var db = new Data();
                userProfile = db.GetCompleteUserProfile(user.Email);

                if (userProfile != null)
                {
                    DisplayUserData();
                }
                else
                {
                    MessageBox.Show("Error loading user profile data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    LoadDefaultData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                LoadDefaultData();
            }
        }

        private void DisplayUserData()
        {
            try
            {
                // User Information
                UsernameDisplay.Text = userProfile.User.Username;
                EmailDisplay.Text = userProfile.User.Email;
                MemberSinceDisplay.Text = userProfile.User.CreatedAt.ToString("MMMM yyyy");

                // User Preferences
                if (userProfile.Preferences != null)
                {
                    FitnessLevelDisplay.Text = CapitalizeFirstLetter(userProfile.Preferences.FitnessLevel);
                    WorkoutDaysDisplay.Text = $"{userProfile.Preferences.AvailableDaysPerWeek} days/week";
                    GoalDisplay.Text = CapitalizeFirstLetter(userProfile.Preferences.GoalName);

                    // Calculate preferred time based on session duration (you can modify this logic)
                    string preferredTime = GetPreferredTimeDisplay(userProfile.Preferences.SessionDurationMinutes);
                    PreferredTimeDisplay.Text = preferredTime;

                    // Show preferences section
                    PreferencesSection.Visibility = Visibility.Visible;
                }
                else
                {
                    // Hide preferences section if no data
                    PreferencesSection.Visibility = Visibility.Collapsed;
                }

                // Workout Statistics
                if (userProfile.Stats != null)
                {
                    WorkoutsCompletedDisplay.Text = userProfile.Stats.WorkoutsCompleted.ToString();
                    CurrentStreakDisplay.Text = $"{userProfile.Stats.CurrentStreak} days";
                    TotalHoursDisplay.Text = $"{userProfile.Stats.TotalHours:F1} hours";
                }
                else
                {
                    WorkoutsCompletedDisplay.Text = "0";
                    CurrentStreakDisplay.Text = "0 days";
                    TotalHoursDisplay.Text = "0.0 hours";
                }

                // Handle missing image gracefully
                HandleProfileImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying user data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadDefaultData()
        {
            // Fallback data if database load fails
            try
            {
                UsernameDisplay.Text = "User";
                EmailDisplay.Text = "No email";
                MemberSinceDisplay.Text = "Unknown";
                FitnessLevelDisplay.Text = "Not set";
                WorkoutDaysDisplay.Text = "Not set";
                GoalDisplay.Text = "Not set";
                PreferredTimeDisplay.Text = "Not set";
                WorkoutsCompletedDisplay.Text = "0";
                CurrentStreakDisplay.Text = "0 days";
                TotalHoursDisplay.Text = "0.0 hours";

                // Hide preferences section
                PreferencesSection.Visibility = Visibility.Collapsed;

                HandleProfileImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading default data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HandleProfileImage()
        {
            try
            {
                // Try to load the image, if it fails, set a fallback
                if (ProfileImageBrush.ImageSource == null)
                {
                    // Create a fallback colored brush
                    ProfileImageBrush.ImageSource = null;
                    // You could also set a solid color fill here instead
                }
            }
            catch (Exception ex)
            {
                // If image loading fails, continue without it
                System.Diagnostics.Debug.WriteLine($"Could not load profile image: {ex.Message}");
            }
        }

        private string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        private string GetPreferredTimeDisplay(int sessionDurationMinutes)
        {
            // You can customize this logic based on your requirements
            // For now, we'll map duration to time preferences
            if (sessionDurationMinutes <= 30)
                return "Quick Session (≤30 min)";
            else if (sessionDurationMinutes <= 60)
                return "Standard Session (30-60 min)";
            else if (sessionDurationMinutes <= 90)
                return "Extended Session (60-90 min)";
            else
                return "Long Session (>90 min)";
        }

        private void UploadPictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // For now, just show a message that the image was selected
                    // You can implement the actual image display logic later
                    MessageBox.Show($"Image selected: {System.IO.Path.GetFileName(openFileDialog.FileName)}",
                                  "Image Upload", MessageBoxButton.OK, MessageBoxImage.Information);

                    // TODO: Save the image path to user profile
                    // You would need to add profile_picture column to users table
                    // and implement UpdateProfilePicture method in Data class
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void GymPlannerBtn_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to gym planner window
            // Pass user data if needed
            // GymPlannerWindow gymPlannerWindow = new GymPlannerWindow(userProfile.User);
            // gymPlannerWindow.Show();
            // this.Close();

            MessageBox.Show("Navigate to Gym Planner", "Info");
        }

        private void ResetPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            // Open password reset dialog or window
            MessageBox.Show("Password reset functionality", "Reset Password");
        }

        private void DeleteAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete your account? This action cannot be undone.",
                "Confirm Account Deletion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Implement account deletion logic
                if (userProfile?.User != null)
                {
                    // TODO: Implement DeleteUser method in Data class
                    // db.DeleteUser(userProfile.User.Id);
                }
                MessageBox.Show("Account deletion functionality", "Delete Account");
            }
        }
    }
}