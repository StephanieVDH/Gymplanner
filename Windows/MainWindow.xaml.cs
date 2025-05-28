using System.Text;
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
        private readonly int CurrentUserId;
        public MainWindow(int userId)
        {
            CurrentUserId = userId;
            InitializeComponent();
        }

        // track login state (optional; you can also use the panels' Visibility)
        private bool _isLoggedIn = false;

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isLoggedIn)
            {
                // 1) show login dialog
                var loginWin = new LoginWindow();
                bool? ok = loginWin.ShowDialog();
                if (ok == true)
                {
                    // 2) swap the visuals in the button
                    LoginPanel.Visibility = Visibility.Collapsed;
                    AvatarPanel.Visibility = Visibility.Visible;
                    _isLoggedIn = true;
                }
                // else: stay on login state
            }
            else
            {
                // 3) already logged in, open profile
                var profileWin = new ProfileWindow();
                profileWin.Owner = this;
                profileWin.Show();
            }
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