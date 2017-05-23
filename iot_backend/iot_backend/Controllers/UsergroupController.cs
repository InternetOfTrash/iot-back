using iot_backend.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iot_backend.Controllers
{
    [RoutePrefix("containers/usergroups")]
    public class UsergroupController : ApiController
    {
        private Service service = Service.GetInstance();

        public UsergroupController()
        {


        }
        [Route("{id}")]
        /// <summary>
        /// Returns a list of all the history of a container
        /// </summary>
        ///  /// <param name="id">Specify the ID of the container in order to retrieve the history</param>
        /// <returns>List of Entry, contains various information like dates and filllevels</returns>
        public List<string> Get(string id)
        {
            return service.GetUserGroup(id);
        }
        
    }
}