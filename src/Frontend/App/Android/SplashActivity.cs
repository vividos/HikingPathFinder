using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;

namespace HikingPathFinder.App.Android
{
    /// <summary>
    /// Splash screen activity; is started first, and can't be navigated back on.
    /// See https://developer.xamarin.com/guides/android/user_interface/creating_a_splash_screen/
    /// </summary>
    [Activity(MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTop)]
    public class SplashActivity : AppCompatActivity
    {
        /// <summary>
        /// Called in the activity lifecycle when the activity is about to be created. Just
        /// switches to the main activity once the Android app has been loaded.
        /// </summary>
        /// <param name="savedInstanceState">instance state; unused</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            HockeyApp.Android.CrashManager.Register(this, Constants.HockeyApp_AppId_Android);

            var intent = new Intent(this, typeof(MainActivity));
            this.StartActivity(intent);
        }

        /// <summary>
        /// Called when the user presses the back button.
        /// </summary>
        public override void OnBackPressed()
        {
            // prevent user from navigating back by not calling base
        }
    }
}
