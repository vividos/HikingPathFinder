using HikingPathFinder.Model;
using System;
using System.Collections.Generic;

namespace HikingPathFinder.DemoData
{
    /// <summary>
    /// Demo data provider
    /// </summary>
    public static class DataProvider
    {
        /// <summary>
        /// Returns app config object
        /// </summary>
        /// <returns>app config object</returns>
        public static AppConfig GetAppConfig()
        {
            return new AppConfig
            {
                LastUpdated = DateTime.Today + TimeSpan.FromHours(8.5),
                Info = GetAppInfo(),
                LocationList = GetLocationList(),
                StaticPageInfoList = GetStaticPageInfoList(),
                PrePlannedToursList = GetPrePlannedTourList()
            };
        }

        /// <summary>
        /// Returns app info data
        /// </summary>
        /// <returns>app info object</returns>
        public static AppInfo GetAppInfo()
        {
            // return test app info
            return new AppInfo
            {
                SiteName = "Hiking Path Finder beta site",
                AreaName = "Spitzingsee hiking area",
                WebsiteAddress = "https://github.com/vividos/HikingPathFinder",
                AreaRectangle = new MapRectangle
                {
                    NorthWest = new MapPoint(47.77, 11.73),
                    SouthEast = new MapPoint(47.57, 12.04)
                },
                StaticPagesTitle = "Hiking tips",
                License = "Creative Commons Attribution-ShareAlike 4.0 International License (CC-BY-SA)",
                LicenseLink = "https://creativecommons.org/licenses/by-sa/4.0/",
            };
        }

        /// <summary>
        /// Returns list of static page infos
        /// </summary>
        /// <returns>list of static page infos</returns>
        public static List<StaticPageInfo> GetStaticPageInfoList()
        {
            return new List<StaticPageInfo>
            {
                new StaticPageInfo
                {
                    Id = "1",
                    Heading = "How to hike properly",
                    Content = "Wear socks and shoes.",
                    PhotoList = new List<PhotoRef>()
                },
            };
        }

        /// <summary>
        /// A railway station
        /// </summary>
        private static Location railwayStation =
            new Location
            {
                Id = Guid.NewGuid().ToString("B"),
                Name = "Bahnhof Neuhaus (Schliersee)",
                Elevation = 809,
                MapLocation = new MapPoint(47.70599, 11.87451),
                Description = "Anschlussmöglichkeiten zum RBO Bus 9562 zum Spitzingsattel",
                IsTourLocation = false,
                Type = LocationType.PublicTransportTrain,
                PhotoList = new List<PhotoRef>(),
                InternetLink = "http://www.bayerischeoberlandbahn.de/strecken-fahrplaene/linie/3-munchen-hbf-holzkirchen-bayrischzell"
            };

        /// <summary>
        /// Finds pre-planned tour from given plan tour parameters
        /// </summary>
        /// <param name="planTourParams">plan tour params</param>
        /// <returns>pre-planned tour, or null when no tour was found</returns>
        public static PrePlannedTour FindPrePlannedTourList(PlanTourParameters planTourParams)
        {
            var prePlannedTourList = DemoData.DataProvider.GetPrePlannedTourList();

            foreach (var prePlannedTour in prePlannedTourList)
            {
                bool startLocationMatches =
                    planTourParams.StartLocation.Id == prePlannedTour.Tour.StartLocation.Id;

                bool endLocationAvailable =
                    planTourParams.EndLocation != null && prePlannedTour.Tour.EndLocation != null;

                bool endLocationMatches =
                    endLocationAvailable &&
                    planTourParams.EndLocation.Id == prePlannedTour.Tour.EndLocation.Id;

                if (startLocationMatches &&
                    (!endLocationAvailable || endLocationMatches))
                {
                    bool hasSameLocations = true;

                    foreach (var locationRef in planTourParams.TourLocationList)
                    {
                        if (prePlannedTour.Tour.LocationList.Find(x => x.Id == locationRef.Id) == null)
                        {
                            hasSameLocations = false;
                            break;
                        }
                    }

                    if (hasSameLocations)
                    {
                        return prePlannedTour;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// A bus station
        /// </summary>
        private static Location busStation =
            new Location
            {
                Id = Guid.NewGuid().ToString("B"),
                Name = "RBO Bus 9562 Bushaltestelle Spitzingsattel",
                Elevation = 1129,
                MapLocation = new MapPoint(47.672138, 11.8862728),
                Description = "Haltestelle am Spitzingsattel. Abfahrtszeiten Richtung Schliersee: 8:42, 11:15, 15:31, 18:02.",
                IsTourLocation = false,
                Type = LocationType.PublicTransportBus,
                PhotoList = new List<PhotoRef>(),
                InternetLink = "http://www.bayerischeoberlandbahn.de/strecken-fahrplaene/linie/3-munchen-hbf-holzkirchen-bayrischzell"
            };

        /// <summary>
        /// Returns location list
        /// </summary>
        /// <returns>location list</returns>
        public static List<Location> GetLocationList()
        {
            return new List<Location>
            {
                summit1,
                summit2,
                alpineHut1,
                alpineHut2,
                pass1,
                railwayStation,
                busStation,
            };
        }

        /// <summary>
        /// A summit
        /// </summary>
        private static Location summit1 =
            new Location
            {
                Id = Guid.NewGuid().ToString("B"),
                Name = "Brecherspitz",
                Elevation = 1685,
                MapLocation = new MapPoint(47.6764385, 11.8710533),
                Description = "Herrliche Aussicht über die drei Seen Schliersee im Norden, Tegernsee im Westen und den Spitzingsee im Süden.",
                IsTourLocation = true,
                Type = LocationType.Summit,
                PhotoList = new List<PhotoRef>(),
                InternetLink = "https://de.wikipedia.org/wiki/Brecherspitz"
            };

        /// <summary>
        /// Another summit
        /// </summary>
        private static Location summit2 =
            new Location
            {
                Id = Guid.NewGuid().ToString("B"),
                Name = "Jägerkamp",
                Elevation = 1746,
                MapLocation = new MapPoint(47.673511, 11.9060494),
                Description = "Gipfel in den Schlierseer Bergen, mit Ausblick auf Schliersee und die umliegenden Berge.",
                IsTourLocation = true,
                Type = LocationType.Summit,
                PhotoList = new List<PhotoRef>(),
                InternetLink = "https://de.wikipedia.org/wiki/J%C3%A4gerkamp"
            };

        /// <summary>
        /// An alpine hut
        /// </summary>
        private static Location alpineHut1 =
            new Location
            {
                Id = Guid.NewGuid().ToString("B"),
                Name = "Ankel-Alm",
                Elevation = 1311,
                MapLocation = new MapPoint(47.6838571, 11.8687695),
                Description = "Privat bewirtschaftete Alm; Montag Ruhetag",
                IsTourLocation = true,
                Type = LocationType.AlpineHut,
                PhotoList = new List<PhotoRef>(),
                InternetLink = string.Empty
            };

        /// <summary>
        /// Another alpine hut
        /// </summary>
        private static Location alpineHut2 =
            new Location
            {
                Id = Guid.NewGuid().ToString("B"),
                Name = "Schönfeldhütte",
                Elevation = 1410,
                MapLocation = new MapPoint(47.66508, 11.90612),
                Description = "Alpenvereinshütte der Sektion München des DAV",
                IsTourLocation = true,
                Type = LocationType.AlpineHut,
                PhotoList = new List<PhotoRef>(),
                InternetLink = "https://www.davplus.de/huetten__wege/bewirtschaftete_huetten/uebersicht/schoenfeldhuette"
            };

        /// <summary>
        /// A mountain pass
        /// </summary>
        private static Location pass1 =
            new Location
            {
                Id = Guid.NewGuid().ToString("B"),
                Name = "Spitzingsattel",
                Elevation = 1129,
                MapLocation = new MapPoint(47.672138, 11.8862728),
                Description = "Sattel auf halbem Wege zwischen Schliersee und Spitzingsee",
                IsTourLocation = true,
                Type = LocationType.Pass,
                PhotoList = new List<PhotoRef>(),
                InternetLink = string.Empty
            };

        /// <summary>
        /// Segment from railway station to alpine hut
        /// </summary>
        private static Segment segmentRailwayStationAlpineHut1 =
            new Segment
            {
                Name = "SB1 Ankelalm-Weg",
                Description = "Starte am Bahnhof und folge den Hinweisschildern zur Ankel-Alm.",
                Rating = "T2",
                LocationStart = LocationRef.FromLocation(railwayStation),
                LocationEnd = LocationRef.FromLocation(alpineHut1),
                Duration = TimeSpan.FromHours(1.5),
                SegmentLengthInKm = 5.2,
                AltitudeUpInMeters = 400,
                AltitudeDownInMeters = 0,
                TrackPointsList = new List<TrackPoint>(),
                PhotoList = new List<PhotoRef>()
            };

        /// <summary>
        /// Segment from alpine hut to summit
        /// </summary>
        private static Segment segmentAlpineHut1Summit1 =
            new Segment
            {
                Name = "SB1 Brecherspitz-Nordgrat",
                Description = "Nach der Ankel-Alm links zum Sattel und durch die Latschen den Nordgrat folgen.",
                Rating = "T3",
                LocationStart = LocationRef.FromLocation(alpineHut1),
                LocationEnd = LocationRef.FromLocation(summit1),
                Duration = TimeSpan.FromHours(1.0),
                SegmentLengthInKm = 6.2,
                AltitudeUpInMeters = 500,
                AltitudeDownInMeters = 0,
                TrackPointsList = new List<TrackPoint>(),
                PhotoList = new List<PhotoRef>()
            };

        /// <summary>
        /// Segment from summit to pass
        /// </summary>
        private static Segment segmentSummit1Pass1 =
            new Segment
            {
                Name = "W21 Trautweinweg",
                Description = "Den Gipfel nach Westen verlassend, steigen wir am Grat ab Richtung Obere Firstalm und zum Spitzingsattel.",
                Rating = "T3",
                LocationStart = LocationRef.FromLocation(summit1),
                LocationEnd = LocationRef.FromLocation(pass1),
                Duration = TimeSpan.FromHours(1.0),
                SegmentLengthInKm = 4.2,
                AltitudeUpInMeters = 0,
                AltitudeDownInMeters = 700,
                TrackPointsList = new List<TrackPoint>(),
                PhotoList = new List<PhotoRef>()
            };

        /// <summary>
        /// Segment from pass to railway station
        /// </summary>
        private static Segment segmentPass1RailwayStation =
            new Segment
            {
                Name = "Alte Spitzingseestraße",
                Description = "Vom Sattel aus in Richtung Norden durch den Wald absteigen.",
                Rating = "T1",
                LocationStart = LocationRef.FromLocation(pass1),
                LocationEnd = LocationRef.FromLocation(railwayStation),
                Duration = TimeSpan.FromHours(1.0),
                SegmentLengthInKm = 3.2,
                AltitudeUpInMeters = 0,
                AltitudeDownInMeters = 200,
                TrackPointsList = new List<TrackPoint>(),
                PhotoList = new List<PhotoRef>()
            };

        /// <summary>
        /// Returns list of pre-planned tours
        /// </summary>
        /// <returns>list of pre-planned tours</returns>
        public static List<PrePlannedTour> GetPrePlannedTourList()
        {
            return new List<PrePlannedTour>
            {
                new PrePlannedTour
                {
                    TourName = "Brecherspitz über den Nordgrat",
                    ShortDescription = @"Der Weg führt von Neuhaus durch den Dürnbachwald,
 vorbei an der Ankelalm und über den Nordgrat zum Gipfel",
                    Tour = new Tour
                    {
                        StartLocation = railwayStation,
                        EndLocation = busStation,

                        TrackLengthInKm = 10.0,
                        AltitudeUpInMeters = 880,
                        Duration = TimeSpan.FromHours(2.5),

                        LocationList = new List<Location>
                        {
                            railwayStation,
                            alpineHut1,
                            summit1,
                            pass1,
                            railwayStation,
                        },

                        SegmentList = new List<Segment>
                        {
                            segmentRailwayStationAlpineHut1,
                            segmentAlpineHut1Summit1,
                            segmentSummit1Pass1,
                            segmentPass1RailwayStation
                        }
                    }
                },
            };
        }
    }
}
