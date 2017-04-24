using HikingPathFinder.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HikingPathFinder.WebApi.Controllers
{
    /// <summary>
    /// Controller for photos that are referenced by a PhotoRef object
    /// </summary>
    [Route("api/[controller]")]
    public class PhotoController : Controller
    {
        /// <summary>
        /// GET api/photo/ref-list
        /// Retrieves a photo object by given photo ref
        /// </summary>
        /// <param name="photoRef">photo reference</param>
        /// <returns>photo object</returns>
        [HttpPost("{ref-list}")]
        public Photo Get([FromBody]PhotoRef photoRef)
        {
            return new Photo
            {
                Ref = photoRef,
                JPEGData = null,
            };
        }

        /// <summary>
        /// GET: api/photo/ref-list
        /// Returns a list of photos for given list of photo refs
        /// </summary>
        /// <param name="photoRefList">list of photo refs</param>
        /// <returns>list of photos</returns>
        [HttpPost("{ref-list}")]
        public IEnumerable<Photo> Get([FromBody]IEnumerable<PhotoRef> photoRefList)
        {
            var photoList =
                from photoRef in photoRefList
                select new Photo
                {
                    Ref = photoRef,
                    JPEGData = null,
                };

            return photoList;
        }
    }
}
