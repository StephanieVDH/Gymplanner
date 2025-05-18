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

namespace Gymplanner.Windows
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string plain = PasswordBox.Password;
            string hashed = BCrypt.Net.BCrypt.HashPassword(plain); // Package moet je nog installeren  (BCrypt.Net-Next)

            var user = new User
            {
                Username = UserNameTextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim(),
                PasswordHash = hashed,
                //DateOfBirth = DobPicker.SelectedDate.HasValue
                 //        ? DateOnly.FromDateTime(DobPicker.SelectedDate.Value):null
            };

            var db = new Data();
            int newId = db.InsertUser(user);
            if (newId > 0)
            {
                // success → open main
                var main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Registration failed—please try again.");
            }


            // TODO: Validate inputs
            // If successful, perhaps open MainWindow:
            // var main = new MainWindow();
            // main.Show();
            // this.Close();
        }
    }
}
