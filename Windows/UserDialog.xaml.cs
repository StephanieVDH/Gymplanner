using System.Windows;
using System.Windows.Controls;
using Gymplanner;
using Gymplanner.CS;

namespace Gymplanner.Windows
{
    public partial class UserDialog : Window
    {
        private readonly Data _dataService = new Data();
        private readonly User _user;
        private readonly bool _isEdit;

        public UserDialog()
        {
            InitializeComponent();
            Title = "Add User";
            _user = new User();
            OkButton.Click += OkButton_Click;
            CancelButton.Click += (s, e) => DialogResult = false;
            RoleComboBox.SelectedIndex = 1; // default to "user"
        }

        public UserDialog(User user) : this()
        {
            Title = "Edit User";
            _isEdit = true;
            _user = user;
            UsernameTextBox.Text = user.Username;
            EmailTextBox.Text = user.Email;
            // leave PasswordBox empty for security
            foreach (ComboBoxItem item in RoleComboBox.Items)
                if (item.Content.ToString() == user.Role)
                    RoleComboBox.SelectedItem = item;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _user.Username = UsernameTextBox.Text;
            _user.Email = EmailTextBox.Text;
            if (!_isEdit && PasswordBox.Password.Length > 0)
                _user.PasswordHash = PasswordBox.Password; // TODO: hash properly
            _user.Role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "user";

            if (_isEdit)
            {
                // TODO: implement Data.UpdateUser(_user)
            }
            else
            {
                _dataService.InsertUser(_user);
            }

            DialogResult = true;
        }
    }
}
