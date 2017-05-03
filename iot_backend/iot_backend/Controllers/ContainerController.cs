using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iot_backend.Controllers
{
    [RoutePrefix("containers/containers/{id}")]
    public class ContainerController : ApiController
    {
        private Service service = Service.GetInstance();

        public ContainerController()
        {


        }

        /// <summary>
        /// Returns a container
        /// </summary>
        ///  /// <param name="id">Specify the ID of the container in order to retrieve the container</param>
        /// <returns>Container with all of its information</returns>
        public Container Get(string id)
        {
            return service.GetContainer(id);
        }


    }
}