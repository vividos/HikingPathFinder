using HikingPathFinder.App.ViewModels;
using System;
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

        /// <summary>
        /// Called when user clicked on the "visit webpage" button
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnClicked_ButtonVisitWebpage(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("https://github.com/vividos/HikingPathFinder"));
        }
    }
}
