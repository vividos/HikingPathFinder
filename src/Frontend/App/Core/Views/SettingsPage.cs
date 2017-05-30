using Xamarin.Forms;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// Page to set settings specific to the app
    /// </summary>
    public class SettingsPage : ContentPage
    {
        /// <summary>
        /// Creates a new settings page
        /// </summary>
        public SettingsPage()
        {
            // The root page of your application
            this.Title = "Hiking Path Finder";
            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "No settings to set yet"
                    }
                }
            };
        }
    }
}
