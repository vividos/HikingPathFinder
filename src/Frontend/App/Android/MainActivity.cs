using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;

namespace HikingPathFinder.App.Android
{
    /// <summary>
    /// Main activity for the Android app
    /// </summary>
    [Activity(Label = "Hiking Path Finder",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /// <summary>
        /// Called in the activity lifecycle when the activity is about to be created. This starts
        /// the Xamarin.Forms based app
        /// </summary>
        /// <param name="bundle">bundle parameter; unused</param>
        protected override void OnCreate(Bundle bundle)
        {
            this.TabLayoutResource = Resource.Layout.Tabbar;
            this.ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            this.LoadApplication(new App());
        }
    }
}
