using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iot_backend.Controllers
{
    [RoutePrefix("containers/usergroups/{id}")]
    public class UsergroupController : ApiController
    {
        private Service service = Service.GetInstance();

        public UsergroupController()
        {


        }

        /// <summary>
        /// Returns a list of all the history of a container
        /// </summary>
        ///  /// <param name="id">Specify the ID of the container in order to retrieve the history</param>
        /// <returns>List of Entry, contains various information like dates and filllevels</returns>
        public List<string> Get(string id)
        {
            return service.GetUserGroup(id);
        }

        /// <summary>
        /// Subscribes an emailaddress to be notified when container is full
        /// </summary>
        /// <param name="id">id of the container to be subscribed to</param>
        /// <param name="email">emailaddress to mail to</param>
        public IHttpActionResult Post(string id, string email)
        {
            try
            {
                
                if (id.Length != 0 && email.Length !=0)
                {
                    
                    if (ModelState.IsValid)
                    {
                        
                        service.SubscribeToUsergroup(id,email);
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

    }
}