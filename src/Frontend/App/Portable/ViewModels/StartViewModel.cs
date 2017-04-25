using HikingPathFinder.App.Logic;
using HikingPathFinder.App.Views;
using HikingPathFinder.Model;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// ViewModel for start page
    /// </summary>
    public class StartViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Command to execute when the "plan tour" button has been clicked
        /// </summary>
        public Command PlanTourCommand { get; private set; }

        /// <summary>
        /// Command to execute when the "explore map" button has been clicked
        /// </summary>
        public Command ExploreMapCommand { get; private set; }

        /// <summary>
        /// Collection of all news items
        /// </summary>
        public ObservableCollection<string> NewsList { get; private set; }

        /// <summary>
        /// Collection of all pre-planned tours
        /// </summary>
        public ObservableCollection<PrePlannedTour> PrePlannedToursList { get; private set; }

        /// <summary>
        /// Command to execute when an item in the pre-planned tour list has been tapped
        /// </summary>
        public Command<PrePlannedTour> PrePlannedTourItemTappedCommand { get; private set; }

        /// <summary>
        /// Creates a new view model object for the start page
        /// </summary>
        public StartViewModel()
        {
            this.NewsList = new ObservableCollection<string>();
            this.PrePlannedToursList = new ObservableCollection<PrePlannedTour>();

            this.SetupBindings();
        }

        /// <summary>
        /// Sets up bindings
        /// </summary>
        private void SetupBindings()
        {
            this.PlanTourCommand = new Command(async () =>
            {
                await App.Navigation.NavigateAsync(typeof(PlanTourPage), true);
            });

            this.ExploreMapCommand = new Command(async () =>
            {
                await App.Navigation.NavigateAsync(typeof(ExploreMapPage), true);
            });

            this.PrePlannedTourItemTappedCommand =
                new Command<PrePlannedTour>(async (prePlannedTour) =>
                {
                    await this.NavigateToPrePlannedTour(prePlannedTour);
                });
        }

        /// <summary>
        /// Navigates to tour page, showing pre-planned tour
        /// </summary>
        /// <param name="prePlannedTour">pre-planned tour to show</param>
        /// <returns>task to wait on</returns>
        private async Task NavigateToPrePlannedTour(PrePlannedTour prePlannedTour)
        {
            await App.Navigation.NavigateAsync(typeof(ShowTourPage), false, prePlannedTour.Tour);
        }

        /// <summary>
        /// Loads data for the view model, asynchronously
        /// </summary>
        /// <returns>task to wait on</returns>
        public async Task LoadData()
        {
            var dataService = ServiceLocator.Current.GetInstance<DataService>();

            var prePlannedToursList = await dataService.GetPrePlannedToursListAsync(CancellationToken.None);
            this.PrePlannedToursList = new ObservableCollection<PrePlannedTour>(prePlannedToursList);

            this.OnPropertyChanged(nameof(this.PrePlannedToursList));
        }

        #region INotifyPropertyChanged implementation
        /// <summary>
        /// Event that gets signaled when a property has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Call this method to signal that a property has changed
        /// </summary>
        /// <param name="propertyName">property name; use C# 6 nameof() operator</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(
                    this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
