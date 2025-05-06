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

namespace Gymplanner
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            // TODO: Validate credentials against database
            // If valid, open MainWindow and close this window
            // var main = new MainWindow();
            // main.Show();
            // this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to registration window
        }
    }
}
