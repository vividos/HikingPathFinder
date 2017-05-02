using HikingPathFinder.App.Logic;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// View model for the plan tour page
    /// </summary>
    public class PlanTourViewModel
    {
        /// <summary>
        /// List of start/stop locations
        /// </summary>
        public ObservableCollection<LocationAutoCompleteViewModel> StartStopLocationList { get; set; }

        /// <summary>
        /// Start location
        /// </summary>
        public LocationAutoCompleteViewModel StartLocation { get; set; }

        /// <summary>
        /// Stop location
        /// </summary>
        public LocationAutoCompleteViewModel EndLocation { get; set; }

        /// <summary>
        /// Creates a new view model object for "plan tour" view.
        /// </summary>
        public PlanTourViewModel()
        {
            this.SetupBindings();
        }

        /// <summary>
        /// Sets up bindings
        /// </summary>
        private void SetupBindings()
        {
            this.StartStopLocationList = new ObservableCollection<LocationAutoCompleteViewModel>();

            Task.Factory.StartNew(async () => { await this.LoadDataAsync(); });
        }

        /// <summary>
        /// Loads data needed for view model
        /// </summary>
        /// <returns>task to wait on</returns>
        private async Task LoadDataAsync()
        {
            var dataService = ServiceLocator.Current.GetInstance<DataService>();

            var locationList = await dataService.GetLocationListAsync(CancellationToken.None);

            this.StartStopLocationList =
                new ObservableCollection<LocationAutoCompleteViewModel>(
                    from location in locationList
                    select new LocationAutoCompleteViewModel(location));
        }
    }
}
