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
    }
}