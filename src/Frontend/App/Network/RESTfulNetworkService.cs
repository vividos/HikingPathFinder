using HikingPathFinder.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HikingPathFinder.App.Network
{
    /// <summary>
    /// Network service providing remote app functions using RESTful Web API calls.
    /// </summary>
    public class RESTfulNetworkService : INetworkService
    {
        /// <summary>
        /// Base path for web service
        /// </summary>
        private string basePath;

        /// <summary>
        /// Bearer token; may be null or empty
        /// </summary>
        private string token = string.Empty;

        /// <summary>
        /// Creates a new RESTful network service
        /// </summary>
        /// <param name="basePath">base path of RESTful service, e.g. https://xxx.yyy.com/ </param>
        public RESTfulNetworkService(string basePath)
        {
            this.basePath = basePath;
        }

        #region API functions (INetworkService implementation)
        /// <summary>
        /// Retrieves application configuration
        /// </summary>
        /// <param name="cancellationToken">token to cancel operation</param>
        /// <returns>app configuration object</returns>
        public async Task<AppConfig> GetAppConfigAsync(CancellationToken cancellationToken)
        {
            try
            {
                AppConfig result = await this.SendRequestAsync<AppConfig>(
                    "api/appConfig",
                    HttpMethod.Get,
                    null,
                    cancellationToken);

                return result;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.ToString());

                return null;
            }
        }

        /// <summary>
        /// Loads photo data by photo reference, as list
        /// </summary>
        /// <param name="photoRefList">list of photo references to load</param>
        /// <param name="cancellationToken">token to cancel operation</param>
        /// <returns>list of photos to the photo references</returns>
        public async Task<List<Photo>> GetPhotosByRefAsync(List<PhotoRef> photoRefList, CancellationToken cancellationToken)
        {
            string parameter = JsonConvert.SerializeObject(photoRefList);

            List<Photo> result = await this.SendRequestAsync<List<Photo>>(
                "api/photo",
                HttpMethod.Get,
                parameter,
                cancellationToken);

            return result;
        }

        /// <summary>
        /// Plans a tour with given parameters and returns planned tour; may throw exception when
        /// no tour could be planned with the given parameters.
        /// </summary>
        /// <param name="planTourParams">parameters to plan tour</param>
        /// <param name="cancellationToken">token to cancel operation</param>
        /// <returns>planned tour</returns>
        public async Task<Tour> PlanTourAsync(PlanTourParameters planTourParams, CancellationToken cancellationToken)
        {
            string parameter = JsonConvert.SerializeObject(planTourParams);

            Tour result = await this.SendRequestAsync<Tour>(
                "api/planTour",
                HttpMethod.Get,
                parameter,
                cancellationToken);

            return result;
        }
        #endregion

        /// <summary>
        /// Sends a request to the RESTful web service, using the API path and optional body,
        /// returning a result object. On error, throws an exception.
        /// </summary>
        /// <typeparam name="T">type of result object</typeparam>
        /// <param name="apiPath">API path; added to base path of RESTful web service</param>
        /// <param name="httpMethod">HTTP method to use</param>
        /// <param name="body">optional HTTP body parameter</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>request result object, or exception</returns>
        private async Task<T> SendRequestAsync<T>(string apiPath, HttpMethod httpMethod, string body, CancellationToken cancellationToken)
             where T : class
        {
            var client = new PortableRest.RestClient();

            if (!string.IsNullOrEmpty(this.token))
            {
                client.AddHeader("Bearer", this.token);
            }

            ////client.JsonSerializerSettings

            string webAddress = this.basePath + apiPath;
            var request = new PortableRest.RestRequest(webAddress, httpMethod);

            if (!string.IsNullOrEmpty(body))
            {
                request.AddParameter(body);
            }

            var response = await client.SendAsync<T>(request, cancellationToken);

            if (response.Exception != null)
            {
                throw response.Exception;
            }

            return response.Content;
        }
    }
}
