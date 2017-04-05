using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// Page that shows the hamburger menu content
    /// </summary>
    public class MenuPage : ContentPage
    {
        /// <summary>
        /// List of menu items
        /// </summary>
        public ListView Menu { get; set; }

        /// <summary>
        /// Creates a new menu page
        /// </summary>
        public MenuPage()
        {
            this.Title = "Menu";
            this.Icon = "icon.png";

            this.Menu = new MenuListView();

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.Black,
                    Text = "Menu",
                }
            };

            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            layout.Children.Add(menuLabel);
            layout.Children.Add(this.Menu);

            this.Content = layout;
        }
    }
}
