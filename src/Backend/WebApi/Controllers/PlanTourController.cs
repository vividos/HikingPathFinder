using HikingPathFinder.Model;
using Microsoft.AspNetCore.Mvc;

namespace HikingPathFinder.WebApi.Controllers
{
    /// <summary>
    /// Controller to return planned tours
    /// </summary>
    [Route("api/[controller]")]
    public class PlanTourController : Controller
    {
        /// <summary>
        /// GET: api/planTour
        /// Plans a tour with given plan tour parameters
        /// </summary>
        /// <param name="planTourParams">plan tour parameters</param>
        /// <returns>planned tour, or exception when tour couldn't be planned</returns>
        [HttpGet]
        public Tour Get([FromBody]PlanTourParameters planTourParams)
        {
            var prePlannedTourList = HikingPathFinder.DemoData.DataProvider.FindPrePlannedTourList(planTourParams);

            if (prePlannedTourList != null)
            {
                return prePlannedTourList.Tour;
            }

            throw new System.Exception("Tour couldn't be calculated");
        }
    }
}
