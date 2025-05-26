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
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();

            // Set a fallback background color for testing
            this.Background = new SolidColorBrush(Colors.White);

            LoadUserData(); // Load user data when window opens
        }

        private void LoadUserData()
        {
            // For now, let's set some sample data to test if the window displays correctly
            // Replace this with your actual user data loading logic later

            try
            {
                UsernameDisplay.Text = "john_doe";
                EmailDisplay.Text = "john@example.com";
                FitnessLevelDisplay.Text = "Intermediate";
                WorkoutDaysDisplay.Text = "4 days/week";
                GoalDisplay.Text = "Muscle Building";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                    // UserService.UpdateProfilePicture(openFileDialog.FileName);
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
            // GymPlannerWindow gymPlannerWindow = new GymPlannerWindow();
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
                MessageBox.Show("Account deletion functionality", "Delete Account");
            }
        }
    }
}