using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WebView), typeof(HikingPathFinder.App.Android.AndroidWebViewRenderer))]

namespace HikingPathFinder.App.Android
{
    /// <summary>
    /// Android custom WebView renderer
    /// See https://xamarinhelp.com/webview-rendering-engine-configuration/
    /// </summary>
    public class AndroidWebViewRenderer : WebViewRenderer
    {
        /// <summary>
        /// Called when web view element has been changed
        /// </summary>
        /// <param name="args">event args for web view change</param>
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> args)
        {
            base.OnElementChanged(args);

            if (this.Control != null &&
                args.NewElement != null)
            {
                this.SetupWebViewSettings();
            }
        }

        /// <summary>
        /// Sets up settings for WebView element
        /// </summary>
        private void SetupWebViewSettings()
        {
            this.Control.Settings.JavaScriptEnabled = true;
            this.Control.Settings.DomStorageEnabled = true;
            ////this.Control.Settings.CacheMode = Android.Webkit.CacheModes.CacheElseNetwork;
        }
    }
}
