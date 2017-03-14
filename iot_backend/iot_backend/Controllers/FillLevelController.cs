using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace iot_backend.Controllers
{
    [RoutePrefix("api/filllevel/{id}")]
    public class FillLevelController : ApiController
    {



        /// <summary>
        /// Initializes a new session and returns the session ID.
        /// </summary>
        /// <remarks>Get a session ID, This method is called everytime a new analysation starts.</remarks>
        /// <returns>Session ID</returns>
        public int Get(string id)
        {
            int result;
            bool parseSuccess = int.TryParse(id, out result);
            if (parseSuccess && result < int.MaxValue && result > 0)
            {
                return Service.GetInstance().GetFillLevel(result);
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
        /// Finalises the session, creates the heatmap and transfers it to the client.
        /// </summary>
        /// <remarks>This terminated the session. This post method will disable the possibility to post new coordinates for this session id.</remarks>
        /// <param name="session">Session Id to be terminated</param>
        public void Post([FromBody]ContainerLevel cl)
        {
            if (cl != null)
            {
                if (ModelState.IsValid)
                {
                    Service.GetInstance().SetFillLevel(cl);
                }else
                {
                     
                }
            }
        }
    }
}