using HikingPathFinder.App.ViewModels;
using HikingPathFinder.Model;
using System.Threading.Tasks;
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
            this.Title = "Hiking Path Finder";

            var viewModel = new StartViewModel();
            Task.Factory.StartNew(viewModel.LoadData);

            this.BindingContext = viewModel;

            this.InitializeComponent();
        }

        /// <summary>
        /// Called when an item was tapped on the pre-planned tours list
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="args">event args</param>
        private void OnItemTapped_PrePlannedToursList(object sender, ItemTappedEventArgs args)
        {
            var viewModel = this.BindingContext as StartViewModel;

            var prePlannedTour = args.Item as PrePlannedTour;
            viewModel.PrePlannedTourItemTappedCommand.Execute(prePlannedTour);
        }
    }
}
