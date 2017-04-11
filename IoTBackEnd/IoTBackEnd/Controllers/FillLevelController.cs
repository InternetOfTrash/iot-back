using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IoTBackEnd.Controllers
{
    [RoutePrefix("containers/filllevel/{id}")]
    public class FillLevelController : ApiController
    {
        private Service service = Service.GetInstance();

        public FillLevelController()
        {


        }

        public FillLevelController(List<ContainerLevel> levels)
        {
            foreach (ContainerLevel level in levels)
            {
                service.SetFillLevel(level);
            }
        }
        /// <summary>
        /// Returns the fill level of the specified ID
        /// </summary>
        /// <remarks>Get the fill level of a specific ID.</remarks>
        /// <param name="id">Specify the ID of the container in order to retrieve the fill level</param>
        /// <returns>Fill Level</returns>
        public ContainerLevel Get(string id)
        {
            int result;
            bool parseSuccess = int.TryParse(id, out result);
            if (parseSuccess && result < int.MaxValue && result > 0)
            {
                return new ContainerLevel(result, service.GetFillLevel(result));
            }
            else
            {
                HttpResponseMessage msg = new HttpResponseMessage();
                msg.StatusCode = HttpStatusCode.BadRequest;
                msg.Content = new StringContent("You have entered an invalid ID");
                throw new HttpResponseException(msg);


                // BadRequest("You have entered an invalid ID");
                // invalid entry
            }


        }

        /// <summary>
        /// Returns a list of all the containers and its fill levels
        /// </summary>
        /// <returns>List of ContainerLevel, contains ID's and corresponding fill levels</returns>
        public List<ContainerLevel> Get()
        {
            return service.GetFillLevels();
        }

        /// <summary>
        /// Adds or changes a ContainerLevel object which consists of an ID and a fill level
        /// </summary>
        /// <param name="cl">ContainerLevel to update, consisting of an ID(int) and FillLevel(int)</param>
        public IHttpActionResult Post([FromBody]ContainerLevel cl)
        {
            try
            {
                // Console.WriteLine("IN POST");
                if (cl != null)
                {
                    // Console.WriteLine("CL NOT NULL");
                    if (ModelState.IsValid)
                    {
                        //Console.WriteLine("[Post] ID: " + cl.ID.ToString() + " level: " + cl.FillLevel.ToString());
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
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unfortunate son..."));
            }
        }
    }
}