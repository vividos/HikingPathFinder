using HikingPathFinder.App.ViewModels;
using Xamarin.Forms;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// List view with menu items for the navigation drawer menu
    /// </summary>
    public class MenuListView : ListView
    {
        /// <summary>
        /// Creates a new list view
        /// </summary>
        public MenuListView()
        {
            var data = new MenuListData();
            this.ItemsSource = data;

            this.VerticalOptions = LayoutOptions.FillAndExpand;
            this.BackgroundColor = Color.Transparent;

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(TextCell.TextProperty, "Title");
            cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
            cell.SetValue(TextCell.TextColorProperty, Color.FromHex(Constants.AppForegroundColorHex));
            cell.SetValue(TextCell.DetailColorProperty, Color.FromHex(Constants.AppBackgroundColorHex));

            this.ItemTemplate = cell;
            this.SelectedItem = data[0];
        }
    }
}
