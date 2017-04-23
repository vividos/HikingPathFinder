using HikingPathFinder.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HikingPathFinder.App.Logic
{
    /// <summary>
    /// Network service providing remote app functions using RESTful Web API calls.
    /// </summary>
    public class RESTfulNetworkService : INetworkService
    {
        /// <summary>
        /// Retrieves application configuration
        /// </summary>
        /// <param name="token">token to cancel operation</param>
        /// <returns>app configuration object</returns>
        public Task<AppConfig> GetAppConfigAsync(CancellationToken token)
        {
            // TODO implement using actual REST service
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads photo data by photo reference, as list
        /// </summary>
        /// <param name="photoRefList">list of photo references to load</param>
        /// <param name="token">token to cancel operation</param>
        /// <returns>list of photos to the photo references</returns>
        public Task<List<Photo>> GetPhotosByRefAsync(List<PhotoRef> photoRefList, CancellationToken token)
        {
            // TODO implement using actual REST service
            throw new NotImplementedException();
        }

        /// <summary>
        /// Plans a tour with given parameters and returns planned tour; may throw exception when
        /// no tour could be planned with the given parameters.
        /// </summary>
        /// <param name="planTourParams">parameters to plan tour</param>
        /// <param name="token">token to cancel operation</param>
        /// <returns>planned tour</returns>
        public Task<Tour> PlanTourAsync(PlanTourParameters planTourParams, CancellationToken token)
        {
            // TODO implement using actual REST service
            throw new NotImplementedException();
        }
    }
}
