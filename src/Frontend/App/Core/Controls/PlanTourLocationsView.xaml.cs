using HikingPathFinder.App.ViewModels;
using HikingPathFinder.Model;
using Xamarin.Forms;

namespace HikingPathFinder.App.Controls
{
    /// <summary>
    /// A ContentView that shows locations for planning tours, such as start/end location and the
    /// tour location list that can be rearranged.
    /// </summary>
    public partial class PlanTourLocationsView : ContentView
    {
        /// <summary>
        /// View model for the content view
        /// </summary>
        private PlanTourLocationsViewModel viewModel;

        #region Bindable properties
        /// <summary>
        /// Property that stores the plan tour parameters
        /// </summary>
        public static readonly BindableProperty PlanTourParametersProperty =
            BindableProperty.Create<PlanTourLocationsView, PlanTourParameters>(p => p.PlanTourParameters, null);

        public static readonly BindableProperty IsStartLocationVisibleProperty =
            BindableProperty.Create<PlanTourLocationsView, bool>(p => p.IsStartLocationVisible, true);

        public static readonly BindableProperty IsEndLocationVisibleProperty =
            BindableProperty.Create<PlanTourLocationsView, bool>(p => p.IsEndLocationVisible, true);
        #endregion

        public PlanTourParameters PlanTourParameters
        {
            get
            {
                return this.viewModel.PlanTourParameters;
            }

            set
            {
                this.viewModel.PlanTourParameters = value;
            }
        }

        public bool IsStartLocationVisible
        {
            get
            {
                return this.viewModel.IsStartLocationVisible;
            }

            set
            {
                this.viewModel.IsStartLocationVisible = value;
            }
        }

        public bool IsEndLocationVisible
        {
            get
            {
                return this.viewModel.IsEndLocationVisible;
            }

            set
            {
                this.viewModel.IsEndLocationVisible = value;
            }
        }

        /// <summary>
        /// Creates a new content view containing the tour parameters
        /// </summary>
        public PlanTourLocationsView()
        {
            this.InitializeComponent();

            this.viewModel = new PlanTourLocationsViewModel();
            this.BindingContext = this.viewModel;
        }
    }
}
