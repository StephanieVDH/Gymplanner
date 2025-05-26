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

namespace Gymplanner.Windows
{
    ///  THIS IS A TEMPLATE STILL NEED TO EDIT IT 
    
    public partial class ProfileWindow : Window
    {
        // Properties to hold user data
        public string Username { get; set; }
        public string Email { get; set; }
        public string FitnessLevel { get; set; }
        public string WorkoutDays { get; set; }
        public string Goal { get; set; }
        public bool HasCompletedQuestionnaire { get; set; }

        public ProfileWindow()
        {
            InitializeComponent();
            //to fill in with data conneciton

            LoadUserData();
        }


        private void LoadUserData()
        {
            // Display user information
            UsernameDisplay.Text = Username ?? "Not logged in";
            EmailDisplay.Text = Email ?? "No email";

            // Show/hide preferences section based on questionnaire completion
            if (HasCompletedQuestionnaire && !string.IsNullOrEmpty(FitnessLevel))
            {
                PreferencesSection.Visibility = Visibility.Visible;
                FitnessLevelDisplay.Text = FitnessLevel;
                WorkoutDaysDisplay.Text = WorkoutDays;
                GoalDisplay.Text = Goal;
            }
            else
            {
                PreferencesSection.Visibility = Visibility.Collapsed;
            }

            // Load saved profile picture if exists
            LoadProfilePicture();
        }

        private void LoadProfilePicture()
        {
            //code to load in profile
            
        }

        private void UploadPictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select Profile Picture",
                Filter = "Image files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Load and display the selected image
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(openFileDialog.FileName);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    ProfileImageBrush.ImageSource = bitmap;

                    // Save the image to app data folder
                    SaveProfilePicture(openFileDialog.FileName);

                    MessageBox.Show("Profile picture updated successfully!", "Success",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading picture: {ex.Message}", "Error",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveProfilePicture(string sourcePath)
        {
            //code to save the picture
        }

        private void GymPlannerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if user has completed questionnaire
                if (!HasCompletedQuestionnaire)
                {
                    var result = MessageBox.Show(
                        "You haven't completed the gym planner questionnaire yet.\n\nWould you like to complete it now to get personalized workout plans?",
                        "Questionnaire Required",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Open questionnaire window
                        // QuestionnaireWindow questionnaireWindow = new QuestionnaireWindow();
                        // questionnaireWindow.ShowDialog();

                        // For now, show a placeholder message
                        MessageBox.Show("Opening questionnaire...", "Navigation",
                                      MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    // Open gym planner window
                    // GymPlannerMainWindow gymPlannerWindow = new GymPlannerMainWindow();
                    // gymPlannerWindow.Show();

                    // For now, show a placeholder message
                    MessageBox.Show("Opening gym planner...", "Navigation",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating to gym planner: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to reset your password?\n\nA password reset email will be sent to your registered email address.",
                "Confirm Password Reset",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Here you would typically call your authentication service
                    // AuthService.SendPasswordResetEmail(Email);

                    MessageBox.Show(
                        $"Password reset instructions have been sent to {Email}\n\nPlease check your email and follow the instructions to reset your password.",
                        "Password Reset Sent",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error sending password reset: {ex.Message}", "Error",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "⚠️ WARNING ⚠️\n\nThis action will permanently delete your account and all associated data including:\n\n• Your profile information\n• Workout plans and history\n• All preferences and settings\n\nThis action CANNOT be undone!\n\nAre you absolutely sure you want to delete your account?",
                "Confirm Account Deletion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Second confirmation
                var finalConfirmation = MessageBox.Show(
                    "This is your final confirmation.\n\nType your username to confirm account deletion.\n\nDo you really want to delete your account permanently?",
                    "Final Confirmation Required",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Stop);

                if (finalConfirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Here you would call your user service to delete the account
                        // UserService.DeleteAccount(Username);

                        // Delete local profile picture
                        DeleteLocalProfileData();

                        MessageBox.Show(
                            "Your account has been successfully deleted.\n\nThank you for using Gym Planner!",
                            "Account Deleted",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                        // Close profile window and return to login
                        this.Close();

                        // Show login window
                        // LoginWindow loginWindow = new LoginWindow();
                        // loginWindow.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting account: {ex.Message}", "Error",
                                      MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void DeleteLocalProfileData()
        {
            // code to delete the data of  an account
        }

        // Method to update user preferences after questionnaire completion
        public void UpdatePreferences(string fitnessLevel, string workoutDays, string goal)
        {
            FitnessLevel = fitnessLevel;
            WorkoutDays = workoutDays;
            Goal = goal;
            HasCompletedQuestionnaire = true;

            LoadUserData(); // Refresh the display
        }
    }
}
