using HikingPathFinder.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// Shows the about page of the app, including version number, a description and some credits.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        /// <summary>
        /// Creates a new about page
        /// </summary>
        public AboutPage()
        {
            this.Title = "About";
            this.BindingContext = new AboutViewModel();

            this.InitializeComponent();
        }
    }
}
