using Android.App;
using HikingPathFinder.App;
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("HikingPathFinder.App.Android")]
[assembly: AssemblyDescription("HikingPathFinder Android app")]
[assembly: AssemblyConfiguration("armeabi-v7a")]
[assembly: ComVisible(false)]

// Add some common permissions; duplicated from AndroidManifest.xml
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]

// Add features
[assembly: UsesFeature("android.hardware.location", Required = false)]
[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
[assembly: UsesFeature("android.hardware.location.network", Required = false)]

// Add metadata
[assembly: MetaData("net.hockeyapp.android.appIdentifier", Value = Constants.HockeyApp_AppId_Android)]
