using HikingPathFinder.App.Logic;
using HikingPathFinder.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// View model for a list that displays all tour locations, with the options to rearrange or
    /// remove the tour locations.
    /// </summary>
    public class PlanTourLocationsViewModel
    {
        /// <summary>
        /// View model for Location objects that can be moved around (up or down) in the plan tour
        /// location list.
        /// </summary>
        public class MovableTourLocationViewModel : LocationViewModel
        {
            public Command MoveLocationUpCommand { get; private set; }

            public Command MoveLocationDownCommand { get; private set; }

            public MovableTourLocationViewModel(Location location)
            : base(location)
            {
                this.MoveLocationDownCommand = new Command(this.MoveLocationDown, this.IsPossibleMoveLocationDown);
                this.MoveLocationUpCommand = new Command(this.MoveLocationUp, this.IsPossibleMoveLocationUp);
            }

            private void MoveLocationDown(object obj)
            {
            }

            private bool IsPossibleMoveLocationDown(object arg)
            {
                return false;
            }

            private void MoveLocationUp(object obj)
            {
            }

            private bool IsPossibleMoveLocationUp(object arg)
            {
                return false;
            }
        }

        /// <summary>
        /// List of locations to display
        /// </summary>
        private List<Location> locationList;

        /// <summary>
        /// The plan tour parameters that contains the start, end and tour locations
        /// </summary>
        public PlanTourParameters PlanTourParameters { get; internal set; }

        /// <summary>
        /// Indicates if start location should be visible
        /// </summary>
        public bool IsStartLocationVisible { get; internal set; }

        /// <summary>
        /// Indicates if end location should be visible
        /// </summary>
        public bool IsEndLocationVisible { get; internal set; }

        /// <summary>
        /// Returns view model of start location
        /// </summary>
        public LocationViewModel StartLocation { get; internal set; }

        /// <summary>
        /// Returns view model of end location
        /// </summary>
        public LocationViewModel EndLocation { get; internal set; }

        /// <summary>
        /// List of tour locations to be displayed in the list
        /// </summary>
        public ObservableCollection<MovableTourLocationViewModel> TourLocationList { get; internal set; }

        /// <summary>
        /// Command to execute in order to remove a location
        /// </summary>
        public Command RemoveLocationCommand { get; internal set; }

        /// <summary>
        /// Creates a new tour location list view model
        /// </summary>
        public PlanTourLocationsViewModel()
        {
            this.SetupBindings();

            Task.Factory.StartNew(this.LoadDataAsync);
        }

        /// <summary>
        /// Sets up bindings for the control
        /// </summary>
        private void SetupBindings()
        {
            this.RemoveLocationCommand = new Command(this.RemoveLocation);
        }

        /// <summary>
        /// Removes a location from the location list
        /// </summary>
        /// <param name="obj"></param>
        private void RemoveLocation(object obj)
        {
            // TODO implement
        }

        /// <summary>
        /// Initializes layout by loading data for tour location list
        /// </summary>
        private async Task LoadDataAsync()
        {
            var dataService = DependencyService.Get<DataService>();

            var userSettings = await dataService.GetUserSettingsAsync(CancellationToken.None);
            this.PlanTourParameters = userSettings.CurrentPlanTourParameters;

            this.locationList = await dataService.GetLocationListAsync(CancellationToken.None);

            this.StartLocation = new LocationViewModel(this.FindLocationByRef(this.PlanTourParameters.StartLocation));
            this.EndLocation = new LocationViewModel(this.FindLocationByRef(this.PlanTourParameters.EndLocation));

            var tourLocationViewModelList =
                from location in this.PlanTourParameters.TourLocationList
                select new MovableTourLocationViewModel(FindLocationByRef(location));

            this.TourLocationList = new ObservableCollection<MovableTourLocationViewModel>(tourLocationViewModelList);
        }

        /// <summary>
        /// Finds Location object by location ref
        /// </summary>
        /// <param name="locationRefToFind">location reference to find; may be null</param>
        /// <returns>found location object, or null when not found</returns>
        private Location FindLocationByRef(LocationRef locationRefToFind)
        {
            if (locationRefToFind == null)
            {
                return null;
            }

            return this.locationList.Find(location => location.Id == locationRefToFind.Id);
        }
    }
}
