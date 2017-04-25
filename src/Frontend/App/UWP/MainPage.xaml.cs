namespace HikingPathFinder.App.UWP
{
    /// <summary>
    /// UWP application main page
    /// </summary>
    public sealed partial class MainPage
    {
        /// <summary>
        /// Creates new main page
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            this.LoadApplication(new HikingPathFinder.App.App());
        }
    }
}
