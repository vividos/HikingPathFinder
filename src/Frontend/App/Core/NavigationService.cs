using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HikingPathFinder.App
{
    /// <summary>
    /// Navigation service for the app; manages navigating to pages from any place in the app.
    /// </summary>
    public class NavigationService
    {
        /// <summary>
        /// Root page that stores the master and detail pages
        /// </summary>
        private readonly MasterDetailPage rootPage;

        /// <summary>
        /// Returns key of current page
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                var navigationPage = this.rootPage.Detail as NavigationPage;
                return navigationPage.CurrentPage.GetType().FullName;
            }
        }

        /// <summary>
        /// Creates a new navigation service
        /// </summary>
        /// <param name="rootPage">root page</param>
        public NavigationService(MasterDetailPage rootPage)
        {
            this.rootPage = rootPage;
        }

        /// <summary>
        /// Navigates to a page with given type and parameters
        /// </summary>
        /// <param name="pageType">page type</param>
        /// <param name="topPage">indicates if new page should become the top page on the
        /// navigation stack</param>
        /// <param name="parameter">parameter object to pass</param>
        public void Navigate(Type pageType, bool topPage = true, object parameter = null)
        {
            var task = this.NavigateAsync(pageType, topPage, parameter);
            task.Wait();
        }

        /// <summary>
        /// Navigates to a page with given type and parameters; async version
        /// </summary>
        /// <param name="pageType">page type</param>
        /// <param name="topPage">indicates if new page should become the top page on the
        /// navigation stack</param>
        /// <param name="parameter">parameter object to pass</param>
        /// <returns>task to wait on</returns>
        public async Task NavigateAsync(Type pageType, bool topPage, object parameter = null)
        {
            Page displayPage;
            if (parameter == null)
            {
                displayPage = (Page)Activator.CreateInstance(pageType);
            }
            else
            {
                displayPage = (Page)Activator.CreateInstance(pageType, parameter);
            }

            if (topPage)
            {
                this.rootPage.Detail = new NavigationPage(displayPage);
            }
            else
            {
                var navigationPage = this.rootPage.Detail as NavigationPage;
                Debug.Assert(navigationPage != null, "detail page must be a navigation page");

                await navigationPage.PushAsync(displayPage);
            }
        }

        #region INavigationService implementation
        /// <summary>
        /// Navigates back one page in the naviation stack
        /// </summary>
        public async void GoBack()
        {
            if (this.CanGoBack())
            {
                var navigationPage = this.rootPage.Detail as NavigationPage;
                await navigationPage.PopAsync();
            }
        }

        /// <summary>
        /// Returns if navigation can go back one page
        /// </summary>
        /// <returns>true when back navigation is possible, false when not</returns>
        public bool CanGoBack()
        {
            var navigationPage = this.rootPage.Detail as NavigationPage;
            return navigationPage?.Navigation?.NavigationStack?.Count > 1;
        }

        /// <summary>
        /// Navigates to page with given page key (use typeof(Xxx).FullName).
        /// </summary>
        /// <param name="pageKey">page key to use</param>
        public void NavigateTo(string pageKey)
        {
            this.NavigateTo(pageKey, null);
        }

        /// <summary>
        /// Navigates to a page with given page key (use typeof(Xxx).FullName) and optional
        /// parameter.
        /// </summary>
        /// <param name="pageKey">page key to use</param>
        /// <param name="parameter">parameter; may be null</param>
        public void NavigateTo(string pageKey, object parameter)
        {
            Type pageType = Type.GetType(pageKey);
            Debug.Assert(pageType != null, "page key must be a valid page type");

            this.Navigate(pageType, false, parameter);
        }
        #endregion
    }
}
