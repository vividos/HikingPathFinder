using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// Page showing a map to explore, with pins for locations to use for tour planning.
    /// </summary>
    public class ExploreMapPage : ContentPage
    {
        /// <summary>
        /// Creates a new maps page to explore
        /// </summary>
        public ExploreMapPage()
        {
            this.InitLayout();
        }

        /// <summary>
        /// Initializes layout by loading map html into web view
        /// </summary>
        private void InitLayout()
        {
            this.Title = "Explore Map";

            var platform = ServiceLocator.Current.GetInstance<IPlatform>();

            string htmlText = platform.LoadTextAsset("map/map.html");

            var htmlSource = new HtmlWebViewSource
            {
                Html = htmlText,
                BaseUrl = platform.WebViewBasePath + "map/"
            };

            var webView = new WebView
            {
                Source = htmlSource,

                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            webView.Navigating += this.OnNavigating_WebView;

            this.Content = webView;
        }

        /// <summary>
        /// Called when web view navigates to a new URL
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnNavigating_WebView(object sender, WebNavigatingEventArgs args)
        {
            //// TODO implement getting action arguments args.Cancel = true;
        }
    }
}
