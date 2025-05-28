using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Gymplanner.CS;
using System.Text.RegularExpressions;

namespace Gymplanner.Windows
{
    public partial class PasswordResetDialog : Window
    {
        private readonly User currentUser;
        private readonly Data database;

        public PasswordResetDialog(User user)
        {
            InitializeComponent();
            currentUser = user;
            database = new Data();

            ResetButton.Content = "RESET PASSWORD";

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
            // Reset button label and hide any prior error
            ResetButton.Content = "RESET PASSWORD";
            HideErrorMessage();

            // Validate inputs
            if (!ValidateInputs())
            {
                // On validation failure, offer to try again
                ResetButton.Content = "TRY AGAIN";
                return;
            }
            try
            {
                // 1) Fetch the stored BCrypt hash for this user
                string storedHash = database.GetPasswordHash(currentUser.Email);
                if (storedHash == null)
                {
                    ShowErrorMessage("Unable to verify current password.");
                    return;
                }

                // 2) Verify the current password
                bool valid = BCrypt.Net.BCrypt.Verify(CurrentPasswordBox.Password, storedHash);
                if (!valid)
                {
                    ShowErrorMessage("Current password is incorrect.");
                    CurrentPasswordBox.Focus();
                    CurrentPasswordBox.SelectAll();
                    return;
                }

                // 3) Hash the new password with BCrypt
                string newHash = BCrypt.Net.BCrypt.HashPassword(NewPasswordBox.Password);

                // 4) Update the database
                if (database.UpdateUserPassword(currentUser.Id, newHash))
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

            // REGEX VAN REGISTER HERGEBRUIKEN
            if (!ValidatePassword(NewPasswordBox.Password))
            {
                ShowErrorMessage("New password must be at least 8 characters long and contain at least one digit.");
                NewPasswordBox.Focus();
                NewPasswordBox.SelectAll();
                return false;
            }

            if (NewPasswordBox.Password != ConfirmPasswordBox.Password)
            {
                ShowErrorMessage("New passwords do not match.");
                ConfirmPasswordBox.Focus();
                ConfirmPasswordBox.SelectAll();
                return false;
            }
            if (CurrentPasswordBox.Password == NewPasswordBox.Password)
            {
                ShowErrorMessage("New password must be different from current password.");
                NewPasswordBox.Focus();
                NewPasswordBox.SelectAll();
                return false;
            }
            return true;
        }

        private bool ValidatePassword(string password)
        {
            // at least 8 characters and at least one digit
            const string pattern = @"^(?=.*\d).{8,}$";
            return Regex.IsMatch(password, pattern);
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

        // Submit on Enter, cancel on Escape
        /*protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ResetButton_Click(this, new RoutedEventArgs());
            else if (e.Key == Key.Escape)
                CancelButton_Click(this, new RoutedEventArgs());

            base.OnKeyDown(e);
        }*/
    }
}