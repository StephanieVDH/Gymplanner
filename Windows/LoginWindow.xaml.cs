﻿using System;
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
        public LoginWindow()
        {
            InitializeComponent();
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

            var main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWin = new RegisterWindow();
            registerWin.Show();
            this.Close();
        }
    }
}
