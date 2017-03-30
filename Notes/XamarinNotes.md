# Xamarin Notes

## Debug keystore

File
%LocalAppData%\Xamarin\Mono for Android\debug.keystore

## Features

### Hamburger menu

A hamburger menu can be done using a DrawerLayout:
https://developer.android.com/training/implementing-navigation/nav-drawer.html

The material design guidelines behind this:
https://material.io/guidelines/patterns/navigation-drawer.html

An implementation using Xamarin:
https://github.com/PumpingCode/Xamarin-NavigationDrawerDemo

How to display DrawerLayout over the ActionBar:
http://stackoverflow.com/questions/26440879/how-do-i-use-drawerlayout-to-display-over-the-actionbar-toolbar-and-under-the-st

## Errors

# The one about material resources error

    ...\obj\Debug\resourcecache\...\res\values-v23\values-v23.xml(6): error APT0000: Error retrieving parent for item: No resource found that matches the given name 'android:TextAppearance.Material.Widget.Button.Inverse'.
    ...\obj\Debug\resourcecache\...\res\values-v23\values-v23.xml(25): error APT0000: Error retrieving parent for item: No resource found that matches the given name 'android:Widget.Material.Button.Colored'.

Fix: I only had Android 5.0 SDK installed. Fixed by installing Android 7.1 SDK (or maybe just the
latest one?). Set "Compile using Android version" to "Use latest version".
Optional: Clear out the NuGet packages folder and the Xamarin packages cache at
%LocalAppData%\Xamarin

# The one about the major java class version number 52

    android.jar(java/lang/Object.class): major version 52 is newer than 51, the highest major version supported by this compiler.

