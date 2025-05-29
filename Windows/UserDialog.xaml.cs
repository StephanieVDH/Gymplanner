using System.Windows;
using Gymplanner.CS;    // Your User POCO
using Gymplanner;       // Your Data service

namespace Gymplanner.Windows
{
    public partial class UserDialog : Window
    {
        private readonly Data _data = new Data();
        private readonly bool _isEdit;

        /// <summary>
        /// The user being created or edited.
        /// </summary>
        public User Current { get; private set; }

        public UserDialog()
        {
            InitializeComponent();

            _isEdit = false;
            Current = new User();

            Title = "Add User";
            RoleComboBox.ItemsSource = new[] { "admin", "user" };
            RoleComboBox.SelectedIndex = 1; // default to "user"

            OkButton.Click += OkButton_Click;
            CancelButton.Click += (s, e) => DialogResult = false;
        }

        public UserDialog(User toEdit) : this()
        {
            _isEdit = true;
            Current = toEdit;

            Title = "Edit User";
            UsernameTextBox.Text = toEdit.Username;
            EmailTextBox.Text = toEdit.Email;
            RoleComboBox.SelectedItem = toEdit.Role;
            // leave PasswordBox empty unless changing
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // 1) Gather inputs
            var username = UsernameTextBox.Text.Trim();
            var email = EmailTextBox.Text.Trim();
            var plainPassword = PasswordBox.Password;      // may be empty
            var roleString = RoleComboBox.SelectedItem as string;

            // 2) Validate
            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(roleString))
            {
                MessageBox.Show(
                    "Username, email, and role are required.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            // 3) Fill POCO
            Current.Username = username;
            Current.Email = email;
            Current.Role = roleString;

            // Map role string to your role IDs (adjust as needed)
            int roleId = roleString == "admin" ? 1 : 2;

            // 4) Persist
            bool success;
            if (_isEdit)
            {
                success = _data.UpdateUser(Current, plainPassword, roleId);
            }
            else
            {
                // InsertUser overload: User, plainPassword, roleId
                int newId = _data.InsertUserAdmin(Current, plainPassword, roleId);
                success = newId > 0;
                if (success)
                    Current.Id = newId;
            }

            if (!success)
            {
                MessageBox.Show(
                    "An error occurred while saving the user.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
                return;
            }

            DialogResult = true;
        }
    }
}
