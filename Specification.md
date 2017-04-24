# Hiking Path Finder Specification

This is the specification document that describes all planned functionality for
the Hiking Path Finder application.

## Audience

The application has the following audiences:

- Users that want to search for tours and plan them using the application
  (called "Users").
- Users that want to add content to the tour database in order to enhance the
  app experience (called "Admins").

## Features

The features are categorized into the following levels, based on their
complexity of handling and implementation: Basic, Intermediate and
Professional.

The features are divided further into features for the following
groups, as described under "Audience": User and Admin.

The features are described in the classical way as user stories:

- As `User` I want to `do something` in order to `have purpose X`.

### Basic Features

- As User I want to view the news page and static pages, in order to inform me
  about the site purpose, general information about hiking and other topics.

- As User I want to view pre-planned tours in order to get to know the hiking
  area better and not having to plan a tour.

- As User I want to show tour details, including a summary, a description,
  related images, stops in between and a GPS track.

- As Admin I want to log into the application in order to modify the experience
  for other users.

- As Admin I want to edit the news page and all available static pages in order
  to provide more information to the User.

### Intermediate Features

- As User I want to plan a tour with one major location, such as a summit, an
  alpine hut or a viewpoint, in order to get all tour details to successfully
  undertake the tour.

- As User I want to explore the map of the hiking area in order to see possible
  locations to hike to, or to see possible pre-planned tours.

- As Admin I want to add a new static page in order to provide more infos about
  a new topic to the User.

- As Admin I want to delete an existent static page in order to remove infos
  not longer relevant to the User.

- As Admin I want to update location infos like description, etc., in order
  to provide more infos for the User.

- As Admin I want to edit tour segment infos like description, etc., in order
  to provide more infos for the User.

- As Admin I want to upload photos for tour locations or for existing hiking
  segments in order to add more infos for the User.

### Professional Features

- As User I want to plan a tour with multiple stops in order to experience more
  complex tours in the hiking area.

- As User I want to export a planned or pre-planned tour as a printable
  document in order to take the tour infos with me while hiking.

- As Admin I want to save a planned tour as a new pre-planned tour in order to
  provide the first-time Users with tours to get an overview over the hiking
  area.

- As Admin I want to upload and edit GPS tracks for single segments or for
  whole tours in order to present the user with more details about the tour.

- As Admin I want to add new locations and segments in order to extend the
  possible plannable tours.

## User Interface

### Navigation

Navigation happens using a Hamburger Menu (3 horizontal bars as app icon) that
displays a menu with the following entries:

- Plan Tour
- Explore Map
- My stored Tours
- News
- Info Pages
- Settings
- About

### Start page

The start page shows two quick-link buttons to the following entries:

- Plan Tour
- Explore Map

The page also shows a list of pre-planned tours available for every user.
Clicking on a tour opens the Show Tour page with the pre-planned tour.

### Plan Tour page

The page allows to configure the planning of a tour, including the following
parameters:

- Start location
- End location (may not be set)
- List of Tour locations to visit
- Speed factor (for faster or slower walkers)
- Maximum tour path rating (to restrict to easier tours)

Start, end and tour locations may be entered using the following ways:

- Using typing with auto-completion
- Using a selection list that is ordered alphabetically and can be filtered
- Using the map to select locations
- Using current location and list of nearest locations

A button starts planning the tour. If the user is not online, a request to
find network service is shown. If an error during server-side calculation
occurs, the error is shown. When a tour was found, the tour is shown in another
page.

The last entered tour planning parameters are stored when the user revisits
next time.

### Show Tour page

Shows a single tour, either a pre-planned tour, a stored tour or a just now
planned tour. The page scrolls down as required by the page entries.

The page contains the following elements:
- Tour title and short description, if any
- Tour summary
- Start location description
- Tour segment description (may occur multiple times)
- End location description (may not occur at all)

The tour summary contains the following infos:
- Tour duration in hh:mm
- Tour distance in km
- Maximum tour path rating
- Altitude in meters to climb up
- Altitude in meters to descend down

The Start, End and Tour locations list contains the following details:
- Location name
- Elevation
- Lat/Long coordinates
- Symbol and text describing the type of the location
- Description of the location
- A list of photo thumbnails that may be shown by clicking on them
- An external link that may open the browser app

The start location also contains a button to start external navigation to the
tour start point.

The Tour segment description contains the following details:
- Tour segment name
- Segment distance in km
- Segment duration in hh:mm
- Tour path rating for this segment
- Altitude in meters to climb up
- Altitude in meters to descend down
- Description text

The page allows the following commands:
- Share the planned tour with other apps
- Export the planned tour using PDF, in order to send or print it

### Show Photo page

The page shows the photo that the user clicked on. Below the photo the
following infos are shown:
- Author name
- Location in Lat/Long coordinates

Next to the location coordinates is a button to show the location of the photo
on the map.

### Explore Map page

The page shows a full-screen map of the hiking area, including the following
infos:

- Start and End locations
- Tour locations
- Current location
- Path lines of pre-planned tours

The locations use pins with distinct icons to visualise the type of location.
When clicked on a pin, shows a popup tooltip with infos and actions of the
location.

The following infos are shown about locations:
- Name
- Location type

The following actions are possible with locations:
- Start/End locations: A "Navigate to" link starting an external navigation
- Start/End locations: A "Use this" link to set the start or end location
- Tour locations: A "Add to tour" link to add the location to a tour.

The selected locations are shown in a popup window that is display when a
toolbar button is clicked. In the popup window the tour planning can be
started, continuing to the Plan Tour page. The selected items can also be
cleared.

When multiple pins are in a single area, a symbol is shown with the number of
items in the area. The user has to zoom in more to interact with a single
item.

The current location is only shown when the user starts the function with a
toolbar command button.

A toolbar command button can be used to start the My Position page, in order
to check location quality.

### My Position page

The page shows the current position as Lat/Long coordinates, and the following
additional infos:

- Current altitude
- Last time the coordinates was found
- Type of the location service used (GPS, mobile network)
- Precision of current position and altitude
- Number of GPS satellites, if used for the location fix
- A small map showing the current location with a pin

The values, including the map location, are updated in real-time, as new
location coordinates are found.

The current position can be shared with other apps using a button.

### Settings page

The following settings can be changed on the page:

- Currently used language
- Lat/Long format, D° MM' SS", D° MM.MMMM" or D.DDDDDD
- Distance format, either meter/kilometer or feet/miles

### About page

The about page shows infos about the app, including the used hiking site. A
button lets the user open the associated web page.

## Technical Specification ##

### Platforms ###

The application should be available for the following platforms:

- Web page that can be displayed with any modern browser and device
  (mobile friendly). Some features may not be available on devices with too
  small displays.

- App for Android/iOS/Windows 10/Windows 10 Mobile in order to plan and view
  tours while hiking on the tour.

### Components ###

The application should be a Client-Server application, with the following
components:

- Backend, consisting of an ASP.NET Core application
- Database access, using SQL Server Express
- Web Service API, using a ASP.NET Web API project
- Frontend, Single Page Application using Angular 2
- Smartphone / Tablet App using Xamarin

### Database model ###

TODO

### RESTful WebAPI

The Web API provided by the backend should provide the following functions.
The API is only used by the App.

#### Basic Features

- Get app configuration, including
  - General App infos
  - List of Start Stop locations
  - List of Tour Locations
  - List of pre-planned Tours
  - List of Static Pages

- Get Photo data
  - List of photo references

#### Intermediate Features

- Plan Tour with parameters
  - Start location
  - Stop location (optional)
  - List of Tour Locations (min. 1)

#### Professional Features

- Export given tour as PDF
