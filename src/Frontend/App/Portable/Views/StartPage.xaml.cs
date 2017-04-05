using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// Start page, shows heading, some quick-start buttons and the news items
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        /// <summary>
        /// Creates a new start page
        /// </summary>
        public StartPage()
        {
            this.InitializeComponent();

            this.Title = "Hiking Path Finder";
        }

        /// <summary>
        /// Called when user clicked on the "Plan Tour" button
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnClicked_ButtonPlanTour(object sender, EventArgs args)
        {
            App.Navigation.Navigate(typeof(PlanTourPage), true);
        }

        /// <summary>
        /// Called when user clicked on the "Explore Map" button
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnClicked_ButtonExploreMap(object sender, EventArgs args)
        {
            App.Navigation.Navigate(typeof(ExploreMapPage), true);
        }
    }
}
