using HikingPathFinder.App.ViewModels;
using HikingPathFinder.Model;
using Xamarin.Forms;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// Shows a single tour
    /// </summary>
    public partial class ShowTourPage : ContentPage
    {
        /// <summary>
        /// Creates a new page to show the given tour
        /// </summary>
        /// <param name="tour">tour to show</param>
        public ShowTourPage(Tour tour)
        {
            this.BindingContext = new ShowTourViewModel(tour);

            this.InitializeComponent();
        }
    }
}
