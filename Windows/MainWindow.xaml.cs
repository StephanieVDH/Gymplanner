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
using Gymplanner.CS;
using Gymplanner.Wizard;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gymplanner.Windows
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient httpClient = new HttpClient();

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
        private User _currentUser;

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isLoggedIn)
            {
                // 1) show login dialog
                var loginWin = new LoginWindow();
                bool? ok = loginWin.ShowDialog();
                if (ok == true)
                {
                    _currentUser = loginWin.LoggedInUser;
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
                var profileWin = new ProfileWindow(_currentUser);
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

        private async void GetRecommendationsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Disable button during API call
                GetRecommendationsButton.IsEnabled = false;
                GetRecommendationsButton.Content = "Loading...";
                RecommendationsTextBlock.Text = "Fetching recommendations...";

                // Parse BMI from textbox
                if (!double.TryParse(BmiTextBox.Text, out double bmi))
                {
                    MessageBox.Show("Please enter a valid BMI value.", "Invalid Input",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Call the fitness API
                var recommendations = await GetFitnessRecommendations(bmi);

                // Display the recommendations
                RecommendationsTextBlock.Text = recommendations;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting recommendations: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                RecommendationsTextBlock.Text = "Failed to get recommendations. Please try again.";
            }
            finally
            {
                // Re-enable button
                GetRecommendationsButton.IsEnabled = true;
                GetRecommendationsButton.Content = "Get Recommendations";
            }
        }

        private async Task<string> GetFitnessRecommendations(double bmi)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://fitness-info-api.p.rapidapi.com/api/recom"),
                Headers =
                {
                    { "x-rapidapi-key", "07e6c45aa7mshe70f0925f288b1cp161395jsnc2e0c667364c" },
                    { "x-rapidapi-host", "fitness-info-api.p.rapidapi.com" },
                },
                Content = new StringContent($"{{\"bmi\":{bmi}}}")
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            };

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                // Try to parse and format the JSON response
                try
                {
                    var jsonResponse = JsonConvert.DeserializeObject(body);
                    return JsonConvert.SerializeObject(jsonResponse, Formatting.Indented);
                }
                catch
                {
                    // If JSON parsing fails, return the raw response
                    return body;
                }
            }
        }
    }
}