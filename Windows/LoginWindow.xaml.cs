using Gymplanner.CS;
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

namespace Gymplanner.Windows
{
    public partial class LoginWindow : Window
    {
        private bool isPasswordVisible = false;
        private bool isUpdatingPassword = false;
        public User? LoggedInUser { get; private set; }
        public LoginWindow()
        {
            InitializeComponent();
        }

        // toggle between PasswordBox and TextBox
        private void PasswordEyeIcon_Click(object sender, MouseButtonEventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
            {
                // show plaintext
                PasswordTextBox.Text = PasswordBox.Password;
                PasswordBox.Visibility = Visibility.Collapsed;
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordTextBox.Focus();
                PasswordTextBox.CaretIndex = PasswordTextBox.Text.Length;
            }
            else
            {
                // hide again
                PasswordBox.Password = PasswordTextBox.Text;
                PasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Visible;
                PasswordBox.Focus();
            }
        }

        // keep the two in sync when the real password changes
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (isUpdatingPassword || PasswordTextBox.Visibility != Visibility.Visible)
                return;

            isUpdatingPassword = true;
            PasswordTextBox.Text = PasswordBox.Password;
            isUpdatingPassword = false;
        }

        // keep the two in sync when the mirror TextBox changes
        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isUpdatingPassword)
                return;

            isUpdatingPassword = true;
            PasswordBox.Password = PasswordTextBox.Text;
            isUpdatingPassword = false;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (email == "")
            {
                MessageBox.Show("Please enter your email.");
                return;
            }
            if (password == "")
            {
                MessageBox.Show("Please enter your password.");
                return;
            }

            var db = new Data();
            string? storedHash = db.GetPasswordHash(email);

            if (storedHash == null)
            {
                MessageBox.Show("Oops, looks like you don't have an account yet.");
                return;
            }

            bool valid = BCrypt.Net.BCrypt.Verify(password, storedHash);
            if (!valid)
            {
                MessageBox.Show("Invalid password.");
                return;
            }
        
            // Get user information for the logged-in user
            var user = db.GetUserByEmailForProfile(email);
            LoggedInUser = user;
            this.DialogResult = true;
            this.Close();

            if (user != null)
            {
                // Pass user data to ProfileWindow
                var profileWindow = new ProfileWindow(user);
                profileWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error retrieving user information.");
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWin = new RegisterWindow();
            registerWin.Show();
            this.Close();
        }
    }
}