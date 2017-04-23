using HikingPathFinder.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HikingPathFinder.App.Logic
{
    /// <summary>
    /// Network service that uses demo data to return data.
    /// </summary>
    public class DemoDataNetworkService : INetworkService
    {
        /// <summary>
        /// Retrieves application configuration by using demo data
        /// </summary>
        /// <param name="token">token to cancel operation</param>
        /// <returns>app info object</returns>
        public Task<AppConfig> GetAppConfigAsync(CancellationToken token)
        {
            AppConfig appConfig = DemoData.DataProvider.GetAppConfig();

            return Task.FromResult(appConfig);
        }

        /// <summary>
        /// Loads photo data by photo reference, as list; using demo data
        /// </summary>
        /// <param name="photoRefList">list of photo references to load</param>
        /// <param name="token">token to cancel operation</param>
        /// <returns>list of photos to the photo references</returns>
        public async Task<List<Photo>> GetPhotosByRefAsync(List<PhotoRef> photoRefList, CancellationToken token)
        {
            var photoList = new List<Photo>();

            foreach (var photoRef in photoRefList)
            {
                photoList.Add(new Photo
                {
                    Ref = photoRef,
                    JPEGData = null,
                });

                token.ThrowIfCancellationRequested();

                await Task.Delay(50);

                token.ThrowIfCancellationRequested();
            }

            return photoList;
        }

        /// <summary>
        /// Plans a tour with given parameters and returns planned tour; may throw exception when
        /// no tour could be planned with the given parameters. Using demo data, only returning
        /// tours with parameters that match a pre-planned tour.
        /// </summary>
        /// <param name="planTourParams">parameters to plan tour</param>
        /// <param name="token">token to cancel operation</param>
        /// <returns>planned tour</returns>
        public Task<Tour> PlanTourAsync(PlanTourParameters planTourParams, CancellationToken token)
        {
            var prePlannedTourList = DemoData.DataProvider.GetPrePlannedTourList();

            foreach (var prePlannedTour in prePlannedTourList)
            {
                if (planTourParams.StartLocation.Id == prePlannedTour.Tour.StartLocation.Id &&
                    planTourParams.EndLocation.Id == prePlannedTour.Tour.EndLocation.Id &&
                    planTourParams.TourLocationList.Count == prePlannedTour.Tour.LocationList.Count)
                {
                    bool hasSameLocations = true;

                    int maxLocationIndex = prePlannedTour.Tour.LocationList.Count;
                    for (int locationIndex = 0; locationIndex < maxLocationIndex; locationIndex++)
                    {
                        if (planTourParams.TourLocationList[locationIndex].Id !=
                            prePlannedTour.Tour.LocationList[locationIndex].Id)
                        {
                            hasSameLocations = false;
                            break;
                        }
                    }

                    if (hasSameLocations)
                    {
                        return Task.FromResult(prePlannedTour.Tour);
                    }
                }
            }

            throw new System.Exception("Tour couldn't be calculated");
        }
    }
}
