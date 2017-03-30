using Xamarin.Forms;

namespace HikingPathFinder.App
{
    /// <summary>
    /// Application class for app
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Creates a new application class
        /// </summary>
        public App()
        {
            // The root page of your application
            var content = new ContentPage
            {
                Title = "Hiking Path Finder",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        }
                    }
                }
            };

            this.MainPage = new NavigationPage(content);
        }

        /// <summary>
        /// Called when app is started
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Called when app is put to sleep
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Called when app is resumed
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
