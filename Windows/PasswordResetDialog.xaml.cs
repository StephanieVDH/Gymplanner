using System;
using System.Collections.Generic;
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
using System.Security.Cryptography;
using Gymplanner.CS;

namespace Gymplanner.Windows
{
    public partial class PasswordResetDialog : Window
    {
        private User currentUser;
        private Data database;

        public PasswordResetDialog(User user)
        {
            InitializeComponent();
            currentUser = user;
            database = new Data();

            // Focus on current password field
            CurrentPasswordBox.Focus();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Hide any previous error messages
                HideErrorMessage();

                // Validate inputs
                if (!ValidateInputs())
                    return;

                // Hash the current password to verify
                string currentPasswordHash = HashPassword(CurrentPasswordBox.Password);

                // Verify current password
                if (!database.VerifyCurrentPassword(currentUser.Id, currentPasswordHash))
                {
                    ShowErrorMessage("Current password is incorrect.");
                    CurrentPasswordBox.Focus();
                    CurrentPasswordBox.SelectAll();
                    return;
                }

                // Hash the new password
                string newPasswordHash = HashPassword(NewPasswordBox.Password);

                // Update password in database
                if (database.UpdateUserPassword(currentUser.Id, newPasswordHash))
                {
                    MessageBox.Show("Password reset successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    DialogResult = true;
                    Close();
                }
                else
                {
                    ShowErrorMessage("Failed to update password. Please try again.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"An error occurred: {ex.Message}");
            }
        }

        private bool ValidateInputs()
        {
            // Check if all fields are filled
            if (string.IsNullOrWhiteSpace(CurrentPasswordBox.Password))
            {
                ShowErrorMessage("Please enter your current password.");
                CurrentPasswordBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(NewPasswordBox.Password))
            {
                ShowErrorMessage("Please enter a new password.");
                NewPasswordBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ConfirmPasswordBox.Password))
            {
                ShowErrorMessage("Please confirm your new password.");
                ConfirmPasswordBox.Focus();
                return false;
            }

            // Check password length
            if (NewPasswordBox.Password.Length < 6)
            {
                ShowErrorMessage("New password must be at least 6 characters long.");
                NewPasswordBox.Focus();
                NewPasswordBox.SelectAll();
                return false;
            }

            // Check if new passwords match
            if (NewPasswordBox.Password != ConfirmPasswordBox.Password)
            {
                ShowErrorMessage("New passwords do not match.");
                ConfirmPasswordBox.Focus();
                ConfirmPasswordBox.SelectAll();
                return false;
            }

            // Check if new password is different from current
            if (CurrentPasswordBox.Password == NewPasswordBox.Password)
            {
                ShowErrorMessage("New password must be different from current password.");
                NewPasswordBox.Focus();
                NewPasswordBox.SelectAll();
                return false;
            }

            return true;
        }

        private void ShowErrorMessage(string message)
        {
            ErrorMessageText.Text = message;
            ErrorMessageText.Visibility = Visibility.Visible;
        }

        private void HideErrorMessage()
        {
            ErrorMessageText.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Hash password using SHA256 (same method as your registration)
        /// Note: In production, consider using BCrypt or similar for better security
        /// </summary>
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Handle Enter key press to submit form
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                ResetButton_Click(this, new RoutedEventArgs());
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                CancelButton_Click(this, new RoutedEventArgs());
            }

            base.OnKeyDown(e);
        }
    }
}