# Hiking Path Finder Specification #

This is the specification document that describes all planned functionality for
the Hiking Path Finder application.

## Audience ##

The application has the following audiences:

- Users that want to search for tours and plan them using the application
  (called "Users").
- Users that want to add content to the tour database in order to enhance the
  app experience (called "Admins").

## Features ##

The features are categorized into the following levels, based on their
complexity of handling and implementation: Basic, Intermediate and
Professional.

The features are divided further into features for the following
groups, as described under "Audience": User and Admin.

The features are described in the classical way as user stories:

- As `User` I want to `do something` in order to `have purpose X`.   

### Basic Features ###

- As User I want to view the news page and static pages, in order to inform me
  about the site purpose, general information about hiking and other topics.

- As User I want to view pre-planned tours in order to get to know the hiking
  area better and not having to plan a tour.

- As User I want to show tour details, including a summary, a description,
  related images, stops in between and a GPS track. 

- As Admin I want to log into the application in order to modify the experience
  for other users. 

- As Admin I waot to edit the news page and all available static pages in order
  to provide more information to the User.

### Intermediate Features ###

- As User I want to plan a tour with one major location, such as a summit, an
  alpine hut or a viewpoint, in order to get all tour details to successfully
  undertake the tour. 

- As User I want to explore the map of the hiking area in order to see possible
  locations to hike to, or to see possible pre-planned tours.

- As Admin I want to add a new static page in order to provide more infos about
  a new topic to the User.

- As Admin I want to delete an existent static page in order to remove infos
  not longer relevant to the User.

- As Admin I want to location infos like description, etc., in order
  to provide more infos for the User.

- As Admin I want to edit tour segment infos like description, etc., in order
  to provide more infos for the User.

- As Admin I want to upload photos for tour locations or for existing hiking
  segments in order to add more infos for the User. 

### Professional Features ###

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
