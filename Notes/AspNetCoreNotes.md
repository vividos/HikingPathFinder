# ASP.NET Core - Notes

## General

Can run under .NET Framework, also under .NET Core (platform independent).

Project types:
- Empty: set up everything up yourself
- Web API: RESTful web services
- Web Application: a web app that uses the MVC User Interface design pattern

## Project Template

Bower - manages client side JavaScript package referemces
Bower.json contains the configuration

Appsettings.config - replacement for the IIS Web.config File.
Contains e.g. db connection, logging config.

Project.json - server side dependencies, other project settings.

## MVC pattern

Controller - handles user requests, modifies Model data and returns a new View

Model - project's data, combined with validation rules. ViewModel can be used
for view-specific models. Service layer to access database using Entity
Framework.

View - UI layer, containing client-side HTML, CSS and JavaScript. Ending in
.cshtml to mix code and markup.

## ASP.NET Controllers

File template "MVC Controller".

Class name becomes web subfolder, method name becomes another subfolder.
Routing set up in Startup.cs determines which method is called.

Method can return type string, or can return View(). Then a View class
corresponding to the method name is returned. Property ViewData can be used to
pass data to View.

## ASP.NET Views

File template "MVC View Page"

.cshtml format

    @* server side comment *@
    @{ // inline code comment }

@varName - access to variables

@ViewData - syntax to get data from controller, key-value pairs

ViewBag like ViewData, for more complex types without need for typecasting.
TempData like ViewData, but isn't reset after a redirect to another
controller.

Tag helpers, to specify controllers in html code, e.g.

    <a href=… asp-controller="ControllerName"
        asp-action="MethodName"

Also: asp-route-xxx, with xxx the name of route parameters.

Views/Shared/_Layout.cshtml contains the default layout for all pages.

## ASP.NET Models

No template, just a .cs file. Connects with Views, use asp-??? tag helpers.

ViewModel: use to map model entities to view

## Web API

Create public endpoints that relate directly to user activities. 

REST based, no SOAP. REST - representational state transfer 

CRUD operations, create, read, update, delete


## Template Web API Controller

Same base class as MVC Controller. Route-attribute defines name of API.

## Entity Framework Core

Code first approach

In memory database during development possible.

DbContext base class for datanase elements, DbSet.

## IoC in ASP.Net Core

Configured in ConfigureServices in Startup.cs

- Transient: new instance is created every time
- Scoped: new instance is created for each new web requrest
- Singleton: new instance is created once at startup
- Instance: as singleton, but instance is created by oneself
