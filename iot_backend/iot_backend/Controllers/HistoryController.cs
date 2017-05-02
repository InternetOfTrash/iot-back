using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iot_backend.Controllers
{
    [RoutePrefix("containers/history/{id}")]
    public class HistoryController : ApiController
    {
        private Service service = Service.GetInstance();

        public HistoryController()
        {


        }


        /// <summary>
        /// Returns a list of all the history of a container
        /// </summary>
        ///  /// <param name="id">Specify the ID of the container in order to retrieve the history</param>
        /// <returns>List of Entry, contains various information like dates and filllevels</returns>
        public List<Entry> Get(string id)
        {
            return service.GetHistory(id);
        }


    }
}