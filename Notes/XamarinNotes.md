# Xamarin Notes

## General

### Debug keystore

Stored in file:

    %LocalAppData%\Xamarin\Mono for Android\debug.keystore

## Xamarin.Forms

PCL based UI programming, consumed by a simple platform specific startup
project.

Xaml dialect to define pages. App.xaml defines resource dictionary,
app.xaml.cs has app lifecycle methods. Pages in xaml or code to show UI.

Xaml compiler can translate xaml into code; XamlCompilation attribute.

### Pages

- Pages - fill the whole screen
- Layouts - organize elements; e.g. StackLayout
- View - single UI elements
- Cells - elements of list views

ContentPage - shows views

NavigationPage - wraps a page and provides back navigation (PushAsync(),
PopAsync())

### Views

TODO

### Bindings

Data from source to target and back.

Source: ViewModel, set by property `BindingContext` of Page.

Target: properties of UI controls. In xaml:

    Text="{Binding PropertyName, Mode=OneWay|TwoWay|OneWayToSource}" 

Implement `INotifyPropertyChanged` to notify target of source value changes.

Binding parameter `StringFormat` to format values like `ToString()`.

Converter: Implement `IValueConverter` to convert from source to target and
back. Use in binding, or specify in `ResourceDictionary` of Page.xaml or
App.xaml.

x:Reference to reference values in other Targets

### Commands

Clicked or Tapped actions from UI controls directly to the ViewModel object.
Implement ICommand or use `Command` or `Command<T>`.

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

### SQLite.Net for Windows Phone 8

Windows Phone 8 uses the WinRT platform. The SQLite database can be used via SQLite.Net using an
extension that has to be installed in Visual Studio:

http://stackoverflow.com/questions/31042772/unable-to-load-dll-sqlite3-in-sqlite-net-platform-winrt#31043793

Search for the extension "SQLite for Windows Phone 8.1", version 3.18.0.

## Errors

### The one about material resources error

    ...\obj\Debug\resourcecache\...\res\values-v23\values-v23.xml(6): error APT0000: Error retrieving parent for item: No resource found that matches the given name 'android:TextAppearance.Material.Widget.Button.Inverse'.
    ...\obj\Debug\resourcecache\...\res\values-v23\values-v23.xml(25): error APT0000: Error retrieving parent for item: No resource found that matches the given name 'android:Widget.Material.Button.Colored'.

Fix: I only had Android 5.0 SDK installed. Fixed by installing Android 7.1 SDK (or maybe just the
latest one?). Set "Compile using Android version" to "Use latest version".
Optional: Clear out the NuGet packages folder and the Xamarin packages cache at
%LocalAppData%\Xamarin

### The one about the major java class version number 52

    android.jar(java/lang/Object.class): major version 52 is newer than 51, the highest major version supported by this compiler.

Solution: Install Java 1.8 JDK, which can handle .class files with major
version 52. Then configure JDK under `Tools > Options > Xamarin > Android
Tools`.

### The one about the missing method AddDrawerListener

Detail text is:

    System.MissingMethodException: Method 'Android.Support.V4.Widget.DrawerLayout.AddDrawerListener' not found.

Solution: Update to the latest Xamarin.Forms assemblies using NuGet. The
support library that Xamarin.Forms is using was not matching the Google SDK
anymore.

### The one about the missing timestamp

Detail error message is:

    No -tsa or -tsacert is provided and this jar is not timestamped. Without a timestamp, users may not be able to validate this jar after the signer certificate's expiration date (2045-08-11) or after any future revocation date.

Solution: https://bugzilla.xamarin.com/show_bug.cgi?id=34699#c3

Use `<JarsignerTimestampAuthorityUrl>` property in the Android solution in order to specify the
URL where timestamps are provided by the certificate authority that is used to sign the app.

### The one about the Code Analysis error CA0001

Detail error message is:

    error : CA0001 : Rule=Microsoft.Usage#CA2214

The error occurs when compiling a Xamarin.Forms project that has Code Analysis active, and has one
or more xaml code-behind classes that are marked [XamlCompilation(XamlCompilationOptions.Compile)]

Solution: Use only Code Analysis or Xaml compilation, not both, until this bug is fixed:
https://bugzilla.xamarin.com/show_bug.cgi?id=44130
