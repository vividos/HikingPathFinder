using HikingPathFinder.App.ViewModels;
using Xamarin.Forms;

namespace HikingPathFinder.App.Views
{
    /// <summary>
    /// Page to start planning a tour by specifying tour start, locations to visit and some more
    /// parameters.
    /// </summary>
    public partial class PlanTourPage : ContentPage
    {
        /// <summary>
        /// Creates new page to plan a tour
        /// </summary>
        public PlanTourPage()
        {
            this.BindingContext = new PlanTourViewModel();

            MessagingCenter.Subscribe<PlanTourViewModel, string>(
                this,
                "DisplayAlert",
                async (sender, args) =>
                {
                    await this.DisplayAlert("Error", args, "OK");
                });

            this.InitializeComponent();
        }
    }
}
