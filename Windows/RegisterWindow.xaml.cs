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
using Gymplanner.CS;
using Gymplanner;
using System.Text.RegularExpressions;

namespace Gymplanner.Windows
{
    public partial class RegisterWindow : Window
    {
        private bool isPasswordVisible = false;
        private bool isConfirmPasswordVisible = false;
        private bool isUpdatingPassword = false;
        private bool isUpdatingConfirmPassword = false;


        public RegisterWindow()
        {
            InitializeComponent();
        }

        // Password visibility toggle
        private void PasswordEyeIcon_Click(object sender, MouseButtonEventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
            {
                // Show password as text
                PasswordTextBox.Text = PasswordBox.Password;
                PasswordBox.Visibility = Visibility.Collapsed;
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordTextBox.Focus();
                PasswordTextBox.CaretIndex = PasswordTextBox.Text.Length;
            }
            else
            {
                // Hide password
                PasswordBox.Password = PasswordTextBox.Text;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Visible;
                PasswordBox.Focus();
            }
        }

        // Confirm Password visibility toggle
        private void ConfirmPasswordEyeIcon_Click(object sender, MouseButtonEventArgs e)
        {
            isConfirmPasswordVisible = !isConfirmPasswordVisible;

            if (isConfirmPasswordVisible)
            {
                // Show password as text
                ConfirmPasswordTextBox.Text = ConfirmPasswordBox.Password;
                ConfirmPasswordBox.Visibility = Visibility.Collapsed;
                ConfirmPasswordTextBox.Visibility = Visibility.Visible;
                ConfirmPasswordTextBox.Focus();
                ConfirmPasswordTextBox.CaretIndex = ConfirmPasswordTextBox.Text.Length;
            }
            else
            {
                // Hide password
                ConfirmPasswordBox.Password = ConfirmPasswordTextBox.Text;
                ConfirmPasswordTextBox.Visibility = Visibility.Collapsed;
                ConfirmPasswordBox.Visibility = Visibility.Visible;
                ConfirmPasswordBox.Focus();
            }
        }

        // Sync password changes between PasswordBox and TextBox
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!isUpdatingPassword)
            {
                isUpdatingPassword = true;
                PasswordTextBox.Text = PasswordBox.Password;
                isUpdatingPassword = false;
            }
        }

        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isUpdatingPassword)
            {
                isUpdatingPassword = true;
                PasswordBox.Password = PasswordTextBox.Text;
                isUpdatingPassword = false;
            }
        }

        // Sync confirm password changes between PasswordBox and TextBox
        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!isUpdatingConfirmPassword)
            {
                isUpdatingConfirmPassword = true;
                ConfirmPasswordTextBox.Text = ConfirmPasswordBox.Password;
                isUpdatingConfirmPassword = false;
            }
        }

        private void ConfirmPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isUpdatingConfirmPassword)
            {
                isUpdatingConfirmPassword = true;
                ConfirmPasswordBox.Password = ConfirmPasswordTextBox.Text;
                isUpdatingConfirmPassword = false;
            }
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            // === Validation Logic ===
            if (!ValidateEmail(EmailTextBox.Text))
            {
                MessageBox.Show("Please enter a valid email address (must contain '@').", "Invalid Email", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!ValidatePassword(PasswordBox.Password))
            {
                MessageBox.Show("Password must be at least 8 characters long and include at least one number.", "Invalid Password", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                MessageBox.Show("Passwords do not match.", "Password Mismatch", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // === Hash password and create user ===
            string plain = PasswordBox.Password;
            string hashed = BCrypt.Net.BCrypt.HashPassword(plain);

            var user = new User
            {
                Username = UserNameTextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim(),
                PasswordHash = hashed,
            };

            // === Insert into database ===
            var db = new Data();
            int newId = db.InsertUser(user);

            if (newId > 0)
            {
                var main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Registration failed—please try again.");
            }
        }

        // === Regex Validation Methods ===
        private bool ValidateEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool ValidatePassword(string password)
        {
            string passwordPattern = @"^(?=.*\d).{8,}$";
            return Regex.IsMatch(password, passwordPattern);
        }
    }
}



// TODO: Validate inputs
// If successful, perhaps open MainWindow:
// var main = new MainWindow();
// main.Show();
// this.Close();



