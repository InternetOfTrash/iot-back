using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iot_backend.Controllers
{
    [RoutePrefix("containers/containers")]
    public class ContainerController : ApiController
    {
        private Service service = Service.GetInstance();

        public ContainerController()
        {


        }

        [Route("get/{id}")]
        /// <summary>
        /// Returns a container
        /// </summary>
        ///  /// <param name="id">Specify the ID of the container in order to retrieve the container</param>
        /// <returns>Container with all of its information</returns>
        public Container Get(string id)
        {
            return service.GetContainer(id);
        }

        [Route("post/")]
        /// <summary>
        /// Adds a new container to the system if id is not existent already
        /// </summary>
        /// <param name="id">unqiue identiefier for new container.</param>
        /// <param name="lat">latitude of new container</param>
        /// <param name="lng">longitude of new container</param>
        public IHttpActionResult Post(string id, float lat, float lng)
        {
            try
            {
                
                if (id.Length != 0)
                {
                    
                    if (ModelState.IsValid)
                    {

                        bool success = service.AddContainer(id, lat, lng);
                        if (success)
                        {
                            return Ok();
                        }
                        
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
                return BadRequest(ModelState);
            }
            catch (ArgumentException e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unfortunate son..."));
            }
        }

        [Route("nearme/{lat}/{lng}")]
        /// <summary>
        /// Returns a list of containers near certain coordinate
        /// </summary>
        /// <param name="lat">Specify the latitude of location</param>
        /// <param name="lng">Specify the longitude of location</param>
        /// <returns>Containers in area of request</returns>
        public List<Container> Get(string lat, string lng)
        {
            return service.GetContainersNearMe(lat, lng);
        }
    }
}