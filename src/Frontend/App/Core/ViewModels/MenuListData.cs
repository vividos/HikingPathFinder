using HikingPathFinder.App.Models;
using HikingPathFinder.App.Views;
using System.Collections.Generic;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// List of menu entries used in the navigation drawer. This ties the app together.
    /// </summary>
    public class MenuListData : List<MenuEntry>
    {
        /// <summary>
        /// Creates a new menu list data instance
        /// </summary>
        public MenuListData()
        {
            this.Add(new MenuEntry
            {
                Title = "Start",
                IconSource = "start.png",
                PageType = typeof(StartPage)
            });

            this.Add(new MenuEntry
            {
                Title = "Plan Tour",
                IconSource = "plan_tour.png",
                PageType = typeof(PlanTourPage)
            });

            this.Add(new MenuEntry
            {
                Title = "Explore Map",
                IconSource = "explore_map.png",
                PageType = typeof(ExploreMapPage)
            });

            this.Add(new MenuEntry
            {
                Title = "Settings",
                IconSource = "settings.png",
                PageType = typeof(SettingsPage)
            });

            this.Add(new MenuEntry
            {
                Title = "About",
                IconSource = "about.png",
                PageType = typeof(AboutPage)
            });
        }

        /// <summary>
        /// This method is used to preserve the page classes during linking in Release mode.
        /// </summary>
        [Preserve]
        private void PreservePages()
        {
#pragma warning disable S1848 // Objects should not be created to be dropped immediately without being used
            new StartPage();
            new PlanTourPage();
            new ExploreMapPage();
            new SettingsPage();
            new AboutPage();
#pragma warning restore S1848 // Objects should not be created to be dropped immediately without being used
        }
    }
}
