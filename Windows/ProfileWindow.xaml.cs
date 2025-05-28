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
using Gymplanner.Wizard; // Add this using statement for WizardWindow
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
            try
            {
                WizardWindow wizardWindow = new WizardWindow(userProfile.User.Id);

                // Set the owner to maintain proper window hierarchy
                wizardWindow.Owner = this;

                // Show as modal dialog - user must complete or cancel wizard
                bool? result = wizardWindow.ShowDialog();

                if (result == true)
                {
                    // Wizard completed successfully
                    MessageBox.Show("Workout plan created successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Refresh the user profile data in case preferences were updated
                    if (userProfile?.User != null)
                    {
                        LoadUserProfile(userProfile.User);
                    }
                }
                else if (result == false)
                {
                    // Wizard was cancelled
                    // No action needed, just return to profile
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Workout Wizard: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (userProfile?.User == null)
                {
                    MessageBox.Show("User data not available. Please try refreshing the profile.",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Open password reset dialog
                PasswordResetDialog resetDialog = new PasswordResetDialog(userProfile.User);
                resetDialog.Owner = this;

                bool? result = resetDialog.ShowDialog();

                if (result == true)
                {
                    // Password was successfully reset
                    // Optionally refresh user data or show additional confirmation
                    LoadUserProfile(userProfile.User);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening password reset dialog: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (userProfile?.User == null)
                {
                    MessageBox.Show("User data not available. Please try refreshing the profile.",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // First confirmation
                MessageBoxResult firstConfirm = MessageBox.Show(
                    "Are you sure you want to delete your account?\n\n" +
                    "This will permanently deactivate your account and you will lose access to:\n" +
                    "• Your workout preferences\n" +
                    "• Your workout history\n" +
                    "• Your profile data\n\n" +
                    "This action cannot be easily undone.",
                    "Confirm Account Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (firstConfirm != MessageBoxResult.Yes)
                    return;

                // Second confirmation for safety
                MessageBoxResult finalConfirm = MessageBox.Show(
                    $"This is your final confirmation.\n\n" +
                    $"Delete account for: {userProfile.User.Email}?\n\n" +
                    "Type your username and click Yes to proceed.",
                    "FINAL CONFIRMATION",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Stop);

                if (finalConfirm == MessageBoxResult.Yes)
                {
                    var db = new Data();

                    // Perform soft delete
                    if (db.SoftDeleteUser(userProfile.User.Id))
                    {
                        MessageBox.Show(
                            "Your account has been successfully deactivated.\n\n" +
                            "Thank you for using our app. If you ever want to reactivate your account, " +
                            "please contact support.",
                            "Account Deleted",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                        // Close this window and return to login
                        // You might want to navigate back to login window here
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Failed to delete account. Please try again or contact support if the problem persists.",
                            "Deletion Failed",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting account: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}