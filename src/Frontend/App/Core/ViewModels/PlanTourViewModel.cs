using HikingPathFinder.App.Logic;
using HikingPathFinder.App.Views;
using HikingPathFinder.Model;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HikingPathFinder.App.ViewModels
{
    /// <summary>
    /// View model for the plan tour page
    /// </summary>
    public class PlanTourViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Logging instance
        /// </summary>
        private static Common.Logging.ILog log = App.GetLogger<PlanTourViewModel>();

        /// <summary>
        /// Backing field for StartLocation
        /// </summary>
        private LocationAutoCompleteViewModel startLocation;

        /// <summary>
        /// Backing field for IsStartLocationRequiredVisible
        /// </summary>
        private bool isStartLocationRequiredVisible;

        /// <summary>
        /// Backing field for EndLocation
        /// </summary>
        private LocationAutoCompleteViewModel endLocation;

        /// <summary>
        /// Backing field for TourLocation
        /// </summary>
        private LocationAutoCompleteViewModel tourLocation;

        /// <summary>
        /// Backing field for IsTourLocationRequiredVisible
        /// </summary>
        private bool isTourLocationRequiredVisible;

        /// <summary>
        /// Backing field for IsPlanningActive
        /// </summary>
        private bool isPlanningActive;

        #region Binding properties
        /// <summary>
        /// List of start/stop locations, also cotanining tour locations
        /// </summary>
        public ObservableCollection<LocationAutoCompleteViewModel> StartStopLocationList { get; set; }

        /// <summary>
        /// List of tour locations, not including start/stop locations
        /// </summary>
        public ObservableCollection<LocationAutoCompleteViewModel> TourLocationList { get; set; }

        /// <summary>
        /// Start location
        /// </summary>
        public LocationAutoCompleteViewModel StartLocation
        {
            get
            {
                return this.startLocation;
            }

            set
            {
                this.startLocation = value;

                if (value != null)
                {
                    this.PlanTourParameters.StartLocation =
                        LocationRef.FromLocation(value.Location);
                }
                else
                {
                    this.PlanTourParameters.StartLocation = null;
                }

                this.OnPropertyChanged(nameof(this.StartLocation));
                this.PlanTourCommand.ChangeCanExecute();
            }
        }

        /// <summary>
        /// Property that indicates if start location parameter is required and must be entered.
        /// </summary>
        public bool IsStartLocationRequiredVisible
        {
            get
            {
                return this.isStartLocationRequiredVisible;
            }

            set
            {
                this.isStartLocationRequiredVisible = value;
                this.OnPropertyChanged(nameof(this.IsStartLocationRequiredVisible));
            }
        }

        /// <summary>
        /// End location
        /// </summary>
        public LocationAutoCompleteViewModel EndLocation
        {
            get
            {
                return this.endLocation;
            }

            set
            {
                this.endLocation = value;

                if (value != null)
                {
                    this.PlanTourParameters.EndLocation =
                        LocationRef.FromLocation(value.Location);
                }
                else
                {
                    this.PlanTourParameters.EndLocation = null;
                }

                this.OnPropertyChanged(nameof(this.EndLocation));
                this.PlanTourCommand.ChangeCanExecute();
            }
        }

        /// <summary>
        /// End location
        /// </summary>
        public LocationAutoCompleteViewModel TourLocation
        {
            get
            {
                return this.tourLocation;
            }

            set
            {
                this.tourLocation = value;

                if (value != null)
                {
                    this.PlanTourParameters.TourLocationList =
                        new List<LocationRef>
                        {
                        LocationRef.FromLocation(value.Location)
                        };
                }
                else
                {
                    this.PlanTourParameters.TourLocationList =
                        new List<LocationRef>();
                }

                this.OnPropertyChanged(nameof(this.TourLocation));
                this.PlanTourCommand.ChangeCanExecute();
            }
        }

        /// <summary>
        /// Property that indicates if tour location parameter is required and must be entered.
        /// </summary>
        public bool IsTourLocationRequiredVisible
        {
            get
            {
                return this.isTourLocationRequiredVisible;
            }

            set
            {
                this.isTourLocationRequiredVisible = value;
                this.OnPropertyChanged(nameof(this.IsTourLocationRequiredVisible));
            }
        }

        /// <summary>
        /// Indicates if planning is currently active (in progress)
        /// </summary>
        public bool IsPlanningActive
        {
            get
            {
                return this.isPlanningActive;
            }

            set
            {
                this.isPlanningActive = value;
                this.OnPropertyChanged(nameof(this.IsPlanningActive));
            }
        }

        /// <summary>
        /// Command to start tour planning
        /// </summary>
        public Command PlanTourCommand { get; set; }

        /// <summary>
        /// Command to reset tour parameters
        /// </summary>
        public ICommand ResetTourParameterCommand { get; set; }
        #endregion

        /// <summary>
        /// Tour parameters to plan a tour; this object is automatically updated when the user
        /// selects tour locations and modifies tour parameters.
        /// </summary>
        public PlanTourParameters PlanTourParameters { get; private set; }

        /// <summary>
        /// Creates a new view model object for "plan tour" view.
        /// </summary>
        public PlanTourViewModel()
        {
            this.isStartLocationRequiredVisible = false;
            this.isTourLocationRequiredVisible = false;

            this.isPlanningActive = false;
            this.PlanTourParameters = new PlanTourParameters();

            this.SetupBindings();
        }

        /// <summary>
        /// Sets up bindings
        /// </summary>
        private void SetupBindings()
        {
            this.StartStopLocationList = new ObservableCollection<LocationAutoCompleteViewModel>();

            this.PlanTourCommand = new Command(
                async () => await this.PlanTourAndNavigateAsync(),
                this.CanExecutePlanTourCommand);

            this.ResetTourParameterCommand = new Command(() =>
            {
                this.StartLocation = null;
                this.EndLocation = null;
                this.TourLocation = null;

                this.IsStartLocationRequiredVisible = false;
                this.IsTourLocationRequiredVisible = false;
            });

            Task.Factory.StartNew(async () => await this.LoadDataAsync());
        }

        /// <summary>
        /// Plans tour and then navigates to the ShowTour page
        /// </summary>
        /// <returns>task to wait on</returns>
        private async Task PlanTourAndNavigateAsync()
        {
            try
            {
                Tour plannedTour = await this.PlanTourAsync(this.PlanTourParameters);

                if (plannedTour != null)
                {
                    await App.Navigation.NavigateAsync(typeof(ShowTourPage), false, plannedTour);
                }
            }
            catch (Exception ex)
            {
                log.Error("error while tour planning and navigation", ex);
            }
        }

        /// <summary>
        /// Plans tour using network service and shows activity indicator while planning. When an
        /// error occured, a message is shown and no tour is returned.
        /// </summary>
        /// <param name="planTourParameters">parameters for planning tour</param>
        /// <returns>planned tour, or null on planning errors</returns>
        private async Task<Tour> PlanTourAsync(PlanTourParameters planTourParameters)
        {
            var networkService = ServiceLocator.Current.GetInstance<INetworkService>();

            try
            {
                this.IsPlanningActive = true;

                var plannedTour = await networkService.PlanTourAsync(planTourParameters, CancellationToken.None);

                return plannedTour;
            }
            catch (Exception ex)
            {
                log.Error("error while planning tour", ex);

                this.IsPlanningActive = false;

                MessagingCenter.Send(
                    this,
                    "DisplayAlert",
                    "Error while planning tour!");
            }
            finally
            {
                this.IsPlanningActive = false;
            }

            return null;
        }

        /// <summary>
        /// Returns if the "plan tour" command can be executed; only when plan tour parameters are
        /// valid.
        /// </summary>
        /// <returns>true when command can be executed, false when not</returns>
        private bool CanExecutePlanTourCommand()
        {
            return this.PlanTourParameters != null &&
                this.PlanTourParameters.IsValid;
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

            this.TourLocationList =
                new ObservableCollection<LocationAutoCompleteViewModel>(
                    from location in locationList
                    where location.IsTourLocation
                    select new LocationAutoCompleteViewModel(location));
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
