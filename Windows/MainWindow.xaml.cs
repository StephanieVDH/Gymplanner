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

                // Parse BMI from textbox with culture-invariant parsing
                string bmiText = BmiTextBox.Text.Replace(',', '.'); // Convert comma to dot
                if (!double.TryParse(bmiText, System.Globalization.NumberStyles.Float,
                                   System.Globalization.CultureInfo.InvariantCulture, out double bmi))
                {
                    MessageBox.Show("Please enter a valid BMI value (e.g., 19.5 or 19,5).", "Invalid Input",
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
                Content = new StringContent($"{{\"bmi\":{bmi.ToString(System.Globalization.CultureInfo.InvariantCulture)}}}")
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            };

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                // Clean up and parse the JSON response
                string cleanResponse = body.Trim();

                // Display only the API recommendation
                string apiRecommendation = ExtractRecommendationFromResponse(cleanResponse);
                string formattedResponse = FormatRecommendation(apiRecommendation);

                return formattedResponse;
            }
        }

        private string ExtractRecommendationFromResponse(string responseBody)
        {
            // Try to extract recommendation from various possible response formats
            string lowerResponse = responseBody.ToLower();

            if (lowerResponse.Contains("weight loss") || lowerResponse.Contains("weightloss"))
            {
                return "weight loss";
            }
            else if (lowerResponse.Contains("weight gain") || lowerResponse.Contains("weightgain"))
            {
                return "weight gain";
            }
            else if (lowerResponse.Contains("maintain") || lowerResponse.Contains("normal"))
            {
                return "maintain weight";
            }
            else if (lowerResponse.Contains("recommendation"))
            {
                // Try to extract from JSON format like {"recommendation":"Weight Loss"}
                int start = responseBody.ToLower().IndexOf("\"recommendation\":\"") + 18;
                if (start > 17)
                {
                    int end = responseBody.IndexOf("\"", start);
                    if (end > start)
                    {
                        return responseBody.Substring(start, end - start).ToLower();
                    }
                }
            }

            // If we can't parse it, return the raw response for debugging
            return responseBody.Trim().Trim('"');
        }

        private string GetBmiCategory(double bmi)
        {
            if (bmi < 18.5) return "Underweight";
            else if (bmi < 25) return "Normal";
            else if (bmi < 30) return "Overweight";
            else return "Obese";
        }

        private string FormatRecommendation(string recommendation)
        {
            // Make the recommendation more descriptive based on the API response
            switch (recommendation.ToLower())
            {
                case "weight loss":
                    return "Focus on weight loss through a combination of cardio exercises and strength training. Consider a caloric deficit diet with proper nutrition guidance.";
                case "weight gain":
                    return "Focus on healthy weight gain through strength training and a caloric surplus diet with adequate protein intake.";
                case "maintain weight":
                    return "Maintain your current weight through regular exercise and a balanced diet. Focus on building muscle and cardiovascular fitness.";
                default:
                    return recommendation; // Return original if it doesn't match expected responses
            }
        }
    }
}