﻿using HikingPathFinder.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HikingPathFinder.App
{
    /// <summary>
    /// Interface to network service providing remote app functions
    /// </summary>
    public interface INetworkService
    {
        /// <summary>
        /// Retrieves application configuration
        /// </summary>
        /// <param name="cancellationToken">token to cancel operation</param>
        /// <returns>app configuration object</returns>
        Task<AppConfig> GetAppConfigAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Loads photo data by photo reference, as list
        /// </summary>
        /// <param name="photoRefList">list of photo references to load</param>
        /// <param name="cancellationToken">token to cancel operation</param>
        /// <returns>list of photos to the photo references</returns>
        Task<List<Photo>> GetPhotosByRefAsync(List<PhotoRef> photoRefList, CancellationToken cancellationToken);

        /// <summary>
        /// Plans a tour with given parameters and returns planned tour; may throw exception when
        /// no tour could be planned with the given parameters.
        /// </summary>
        /// <param name="planTourParams">parameters to plan tour</param>
        /// <param name="cancellationToken">token to cancel operation</param>
        /// <returns>planned tour</returns>
        Task<Tour> PlanTourAsync(PlanTourParameters planTourParams, CancellationToken cancellationToken);
    }
}
