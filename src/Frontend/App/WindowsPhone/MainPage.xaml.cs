using Windows.UI.Xaml.Navigation;

namespace HikingPathFinder.App.WindowsPhone
{
    /// <summary>
    /// Main page for the Windows Phone app
    /// </summary>
    public sealed partial class MainPage
    {
        /// <summary>
        /// Creates a new main page
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.LoadApplication(new HikingPathFinder.App.App());
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="args">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            // Xamarin.Forms handles page navigation
        }
    }
}
