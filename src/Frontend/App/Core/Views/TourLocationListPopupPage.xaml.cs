using HikingPathFinder.App.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// Popup page that shows the tour location list, containing start, end and tour locations,
    /// and buttons to start tour planning and to reset the list.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TourLocationListPopupPage : PopupPage
    {
        /// <summary>
        /// Creates a new popup page for tour location list
        /// </summary>
        public TourLocationListPopupPage()
        {
            this.CloseWhenBackgroundIsClicked = true;

            this.frameContainer.BindingContext = new PlanTourLocationsViewModel();

            this.InitializeComponent();
        }

        /// <summary>
        /// Called when user tapped on the close button; closes the popup
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnCloseButtonTapped(object sender, EventArgs args)
        {
            this.CloseAllPopups();
        }

        /// <summary>
        /// Closes all popups
        /// </summary>
        private async void CloseAllPopups()
        {
            await PopupNavigation.PopAllAsync();
        }
    }
}
