using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using IOPath = System.IO.Path;
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
using Gymplanner.Wizard; 
using static Gymplanner.CS.User;
using System.Diagnostics;

namespace Gymplanner.Windows
{
    public partial class ProfileWindow : Window
    {
        private readonly Data data = new Data();
        private UserProfile userProfile;

        // Constructor that accepts user data
        public ProfileWindow(User user)
        {
            InitializeComponent();
            LoadUserProfile(user);
            DataContext = userProfile;
            LoadCurrentPicture();

            // Show AdminPage button only for role_id == 1
            if (userProfile?.User?.RoleId == 1)
                AdminPageBtn.Visibility = Visibility.Visible;
            else
                AdminPageBtn.Visibility = Visibility.Collapsed;
        }

        private void AdminPageBtn_Click(object sender, RoutedEventArgs e)
        {
            var admin = new AdminPage { Owner = this };
            admin.Show();
        }

        private void LoadUserProfile(User user)
        {
            try
            {
                userProfile = data.GetCompleteUserProfile(user.Email);

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
                // Hide preferences section
                PreferencesSection.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading default data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (sessionDurationMinutes == 30)
                return "Quick Session (30 min)";
            else if (sessionDurationMinutes <= 90)
                return "Standard Session (45-60 min)";
            else 
                return "Long Session (90 min)";
        }

        private void LoadCurrentPicture()
        {
            // 1) Start with the default embedded brush
            ProfileImageEllips.Fill = (Brush)FindResource("DefaultAvatarBrush");

            // 2) Fetch the relative picture path from DB
            var rel = data.GetUserProfilePicture(userProfile.User.Id);
            if (string.IsNullOrEmpty(rel))
                return;  // no custom pic, stick with default

            // 3) Build absolute disk path
            var absolute = IOPath.Combine(AppDomain.CurrentDomain.BaseDirectory, rel);
            if (!File.Exists(absolute))
                return;  // file missing, still use default

            // 4) Load and apply the custom image
            var bmp = new BitmapImage(new Uri(absolute, UriKind.Absolute));
            ProfileImageEllips.Fill = new ImageBrush(bmp);
        }

        private void UploadPictureBtn_Click(object sender, RoutedEventArgs e)
        {
            // 1) Let user pick an image
            var dlg = new OpenFileDialog
            {
                Title = "Select Profile Picture",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Multiselect = false
            };
            if (dlg.ShowDialog() != true)
                return;

            // 2) Copy to your app's Images/ProfilePics folder
            var imagesDir = IOPath.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Images",
                "ProfilePics"
            );
            Directory.CreateDirectory(imagesDir);

            // 3) Build a unique filename and copy
            var filename = $"{userProfile.User.Id}{IOPath.GetExtension(dlg.FileName)}";
            var destPath = IOPath.Combine(imagesDir, filename);
            File.Copy(dlg.FileName, destPath, overwrite: true);

            // 4) Persist the *relative* path in the DB
            var relativePath = IOPath.Combine("Images", "ProfilePics", filename);
            var success = data.UpdateUserProfilePicture(
                userProfile.User.Id,
                relativePath
            );

            if (!success)
            {
                MessageBox.Show("Failed to save your picture. Please try again.",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 5) Update the VM so any bindings stay in sync
            userProfile.Picture = relativePath;

            // 6) Update the Ellipse Fill directly
            try
            {
                var absolutePath = IOPath.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    relativePath
                );
                // create a new ImageBrush and assign
                var brush = new ImageBrush(
                    new BitmapImage(new Uri(absolutePath, UriKind.Absolute))
                )
                {
                    Stretch = Stretch.UniformToFill
                };
                ProfileImageEllips.Fill = brush;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error setting profile image: {ex}");
                // fallback to embedded default
                ProfileImageEllips.Fill = (Brush)FindResource("DefaultAvatarBrush");
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
                    "• Your profile data\n\n" +
                    "This action cannot be undone.",
                    "Confirm Account Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (firstConfirm != MessageBoxResult.Yes)
                    return;

                // Second confirmation for safety
                MessageBoxResult finalConfirm = MessageBox.Show(
                    $"This is your final confirmation.\n\n" +
                    $"Delete account for: {userProfile.User.Email}?\n\n" +
                    "Click Yes to proceed.",
                    "FINAL CONFIRMATION",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Stop);

                if (finalConfirm == MessageBoxResult.Yes)
                {
                    if (data.DeleteUser(userProfile.User.Id))
                    {
                        MessageBox.Show("Your account has been successfully deleted");
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