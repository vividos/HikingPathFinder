using HikingPathFinder.App.Models;
using System;
using Xamarin.Forms;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// The root page of the app, showing a slide-out menu on the left, and a navigatable page in
    /// the center of the screen.
    /// </summary>
    public class RootPage : MasterDetailPage
    {
        /// <summary>
        /// Creates a new root page
        /// </summary>
        public RootPage()
        {
            this.InitLayout();
        }

        /// <summary>
        /// Initializes layout of page
        /// </summary>
        private void InitLayout()
        {
            this.MasterBehavior = MasterBehavior.Popover;

            var menuPage = new MenuPage();
            menuPage.Menu.ItemSelected += this.OnMenuItemSelected;

            this.Master = menuPage;
            this.Master.BackgroundColor = Color.FromHex("aaaaaa");

            this.Detail = new NavigationPage(new StartPage());
        }

        /// <summary>
        /// Called when a menu item has been selected; navigates to a new page
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var menuEntry = args.SelectedItem as MenuEntry;

            this.NavigateTo(menuEntry.PageType);
        }

        /// <summary>
        /// Navigates to a new page
        /// </summary>
        /// <param name="pageType">type page to show</param>
        private void NavigateTo(Type pageType)
        {
            Page displayPage = (Page)Activator.CreateInstance(pageType);

            this.Detail = new NavigationPage(displayPage);

            this.IsPresented = false;
        }
    }
}
