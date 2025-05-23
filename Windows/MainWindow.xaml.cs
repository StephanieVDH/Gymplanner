﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gymplanner;
using Gymplanner.Wizard;

namespace Gymplanner.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var loginWin = new LoginWindow(); // creëert nieuw login window
            loginWin.Owner = this;
            loginWin.ShowDialog(); // toont het login window
        }

        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to user profile
        }

        private void StartWizard_Click(object sender, RoutedEventArgs e)
        {
            // Optionally hide the main window:
            // this.Hide();

            var wizard = new WizardWindow();
            wizard.Owner = this;             // sets MainWindow as the owner
            wizard.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wizard.ShowDialog();             // modal

            // After wizard closes, you can Show() or Close() this again:
            // this.Show();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            // Open as a standalone window
            var admin = new AdminPage();
            admin.Owner = this;    // optional: set owner so it stays on top
            admin.Show();          // or admin.ShowDialog() if you want it modal
        }
    }
}