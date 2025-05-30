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

        private async void GetQuoteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Disable button during API call
                GetQuoteButton.IsEnabled = false;
                GetQuoteButton.Content = "Loading...";
                QuoteTextBlock.Text = "Fetching motivational quote...";

                // Call the motivation quotes API
                var quote = await GetMotivationQuote();

                // Display the quote
                QuoteTextBlock.Text = quote;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting quote: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                QuoteTextBlock.Text = "Failed to get quote. Please try again.";
            }
            finally
            {
                // Re-enable button
                GetQuoteButton.IsEnabled = true;
                GetQuoteButton.Content = "Get Motivational Quote";
            }
        }

        private async Task<string> GetMotivationQuote()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://motivation-quotes4.p.rapidapi.com/api"),
                Headers =
                {
                    { "x-rapidapi-key", "07e6c45aa7mshe70f0925f288b1cp161395jsnc2e0c667364c" },
                    { "x-rapidapi-host", "motivation-quotes4.p.rapidapi.com" },
                },
            };

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                // Parse and format the quote response
                return ParseQuoteResponse(body);
            }
        }

        private string ParseQuoteResponse(string responseBody)
        {
            try
            {
                // The API likely returns JSON, try to extract quote and author
                string cleanResponse = responseBody.Trim();

                // Look for common JSON patterns for quotes
                if (cleanResponse.Contains("\"quote\"") && cleanResponse.Contains("\"author\""))
                {
                    string quote = ExtractJsonValue(cleanResponse, "quote");
                    string author = ExtractJsonValue(cleanResponse, "author");

                    if (!string.IsNullOrEmpty(quote))
                    {
                        return string.IsNullOrEmpty(author) ? $"\"{quote}\"" : $"\"{quote}\"\n\n- {author}";
                    }
                }

                // If standard parsing fails, try other common patterns
                if (cleanResponse.Contains("\"text\""))
                {
                    string quote = ExtractJsonValue(cleanResponse, "text");
                    if (!string.IsNullOrEmpty(quote))
                    {
                        return $"\"{quote}\"";
                    }
                }

                // If all parsing fails, return cleaned response
                return cleanResponse.Trim('"').Trim();
            }
            catch
            {
                // If parsing fails, return the raw response
                return responseBody.Trim();
            }
        }

        private string ExtractJsonValue(string json, string key)
        {
            try
            {
                string pattern = $"\"{key}\":\"";
                int start = json.IndexOf(pattern);
                if (start == -1) return string.Empty;

                start += pattern.Length;
                int end = json.IndexOf("\"", start);
                if (end == -1) return string.Empty;

                return json.Substring(start, end - start);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}