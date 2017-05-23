using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iot_backend.Controllers
{
    [RoutePrefix("filllevel")]
    public class FillLevelController : ApiController
    {
        private Service service = Service.GetInstance();

        public FillLevelController()
        {


        }
        [Route("{id}")]
        /// <summary>
        /// Returns the fill level of the specified ID
        /// </summary>
        /// <remarks>Get the fill level of a specific ID.</remarks>
        /// <param name="id">Specify the ID of the container in order to retrieve the fill level</param>
        /// <returns>Fill Level</returns>
        public ContainerLevel Get(string id)
        {
           
            if (id.Length > 0)
            {
                return new ContainerLevel(id, service.GetFillLevel(id));
            }
            else
            {
                HttpResponseMessage msg = new HttpResponseMessage();
                msg.StatusCode = HttpStatusCode.BadRequest;
                msg.Content = new StringContent("You have entered an invalid ID");
                throw new HttpResponseException(msg);
            }

        }
        [Route("")]
        /// <summary>
        /// Returns a list of all the containers and its fill levels
        /// </summary>
        /// <returns>List of ContainerLevel, contains ID's and corresponding fill levels</returns>
        public List<ContainerLevel> Get()
        {
            return service.GetFillLevels();
        }
        [Route("")]
        /// <summary>
        /// Adds or changes a ContainerLevel object which consists of an ID and a fill level which can be retrieved from the json found as a parameter
        /// </summary>
        /// <param name="jsonBody">body containing json, retrieved from a incomming post request.</param>
        public IHttpActionResult Post([FromBody]JSONBody jsonBody)
        {
            try
            {
                if (jsonBody != null)
                {
                     if (ModelState.IsValid)
                    {
                        ContainerLevel cl = new ContainerLevel(jsonBody.dev_id, jsonBody.payload_fields.fill_level);
                        service.SetFillLevel(cl);
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