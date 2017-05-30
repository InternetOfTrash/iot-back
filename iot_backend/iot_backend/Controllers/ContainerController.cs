using iot_backend.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iot_backend.Controllers
{
    [RoutePrefix("containers")]
    public class ContainerController : ApiController
    {
        private Service service = Service.GetInstance();

        public ContainerController()
        {


        }
        [Route("")]
        /// <summary>
        /// Returns a list of containers near certain coordinate
        /// </summary>
        /// <param name="lat">Specify the latitude of location</param>
        /// <param name="lng">Specify the longitude of location</param>
        /// <returns>Containers in area of request</returns>
        public List<Container> Get()
        {
            List<Container> list = service.GetContainers();
            return list;
            //return service.GetContainersNearMe(lat, lng);
        }

        [Route("{id}")]
        /// <summary>
        /// Returns a container
        /// </summary>
        ///  /// <param name="id">Specify the ID of the container in order to retrieve the container</param>
        /// <returns>Container with all of its information</returns>
        public Container Get(string id)
        {
            return service.GetContainer(id);
        }

        [Route("getabove/{percentage}")]
        /// <summary>
        /// Returns a list of containers with a fillevel above specified percentage
        /// </summary>
        ///  /// <param name="percentage">Specify the minimum percentage of the filllevel</param>
        /// <returns>List of containers with a fillevel above specified percentage</returns>
        public List<Container> Get(int percentage)
        {
            return service.GetContainerAbovePercentage(percentage);
        }

        [Route("")]
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
            List<Container> list = service.GetContainersNearMe(lat, lng);
            return list;
            //return service.GetContainersNearMe(lat, lng);
        }


        [Route("subscribe")]
        /// <summary>
        /// Subscribes an emailaddress to be notified when container is full
        /// </summary>
        /// <param name="body">id of the container to be subscribed to and email</param>
        public IHttpActionResult Post([FromBody]UserGroupJsonBody body)
        {
            try
            {
                string id = body.ID;
                string email = body.EMAIL;
                if (id.Length != 0 && email.Length != 0)
                {

                    if (ModelState.IsValid)
                    {

                        service.SubscribeToUsergroup(id, email);
                        return Ok();
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

        [Route("unsubscribe/{id}/{email}/{dummy}")]
        /// <summary>
        /// Subscribes an emailaddress to be notified when container is full
        /// </summary>
        /// <param name="body">id of the container to be subscribed to and email</param>
        public String Get(string id, string email, int dummy)
        {
            try
            {

                if (id.Length != 0 && email.Length != 0)
                {

                    if (ModelState.IsValid)
                    {

                        service.UnsubscribeToUsergroup(id, email);
                        return "Succesfully unsubscribed from container!";
                    }
                    else
                    {
                        return "Unsubscribing from container failed!";
                    }
                }
                return "Unsubscribing from container failed!";
            }
            catch (ArgumentException e)
            {
                return "Unsubscribing from container failed!";
            }
        }

    }
}