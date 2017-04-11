using DBreeze;
using Restup;
using Restup.Webserver.Attributes;
using Restup.Webserver.File;
using Restup.Webserver.Http;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using Restup.Webserver.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace iot_backend2
{
    public class Main : IBackgroundTask
    {


        //public async Task Run()
        //{
        //    var restRouteHandler = new RestRouteHandler();
        //    restRouteHandler.RegisterController<ParameterController>();

        //    var configuration = new HttpServerConfiguration()
        //      .ListenOnPort(11001)
        //      .RegisterRoute("api", restRouteHandler)
        //      .EnableCors();

        //    var httpServer = new HttpServer(configuration);
        //    await httpServer.StartServerAsync();
        //    while (true)
        //    {

        //    }
        //    // now make sure the app won't stop after this (eg use a BackgroundTaskDeferral)
        //}

        private HttpServer _httpServer;

        private BackgroundTaskDeferral _deferral;

        /// <remarks>
        /// If you start any asynchronous methods here, prevent the task
        /// from closing prematurely by using BackgroundTaskDeferral as
        /// described in http://aka.ms/backgroundtaskdeferral
        /// </remarks>
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            // This deferral should have an instance reference, if it doesn't... the GC will
            // come some day, see that this method is not active anymore and the local variable
            // should be removed. Which results in the application being closed.
            _deferral = taskInstance.GetDeferral();
            var restRouteHandler = new RestRouteHandler();

            restRouteHandler.RegisterController<ParameterController>();

            var configuration = new HttpServerConfiguration()
                .ListenOnPort(11001)
                .RegisterRoute("api", restRouteHandler)
                .RegisterRoute(new StaticFileRouteHandler(@"Restup.DemoStaticFiles\Web"))
                .EnableCors(); // allow cors requests on all origins
            //  .EnableCors(x => x.AddAllowedOrigin("http://specificserver:<listen-port>"));

            var httpServer = new HttpServer(configuration);
            _httpServer = httpServer;

            await httpServer.StartServerAsync();

            // Dont release deferral, otherwise app will stop
        }


    }

    public class DataReceived
    {
        public int ID { get; set; }
        public int FillLevel { get; set; }
    }

    [RestController(InstanceCreationType.Singleton)]
    public class ParameterController
    {
        Service service = null;
        public ParameterController()
        {
            service = new Service();
        }
        [UriFormat("/containers/{id}/filllevel/{flevel}")]
        public IPostResponse Post(int id, int flevel)
        {
            try
            {
                // Console.WriteLine("IN POST");
                if (id > 0)
                {

                    // Console.WriteLine("CL NOT NULL");
                    //  if (ModelState.IsValid)
                    //  {
                    //Console.WriteLine("[Post] ID: " + cl.ID.ToString() + " level: " + cl.FillLevel.ToString());
                    ContainerLevel cl = new ContainerLevel(id, flevel);
                    service.SetFillLevel(cl);
                    return new PostResponse(PostResponse.ResponseStatus.Created);
                    //   }
                    //  else
                    //  {
                    // return BadRequest(ModelState);
                    //}
                }
                // return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                // return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unfortunate son..."));
            }

            return new PostResponse(PostResponse.ResponseStatus.Conflict);
        }
    
    [UriFormat("/containers/{id}")]
    public IGetResponse GetWithSimpleParameters(int id)
    {

            try
            {
                // Console.WriteLine("IN POST");
                if (id > 0)
                {

                    // Console.WriteLine("CL NOT NULL");
                    //  if (ModelState.IsValid)
                    //  {
                    //Console.WriteLine("[Post] ID: " + cl.ID.ToString() + " level: " + cl.FillLevel.ToString());
                    //service.GetFillLevel(id);
                    return new GetResponse(
         GetResponse.ResponseStatus.OK,
         new DataReceived() { ID = id, FillLevel = service.GetFillLevel(id)});
                    //   }
                    //  else
                    //  {
                    // return BadRequest(ModelState);
                    //}
                }
                // return BadRequest(ModelState);
            }
            catch (Exception e)
            {
                // return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unfortunate son..."));
            }
            return new GetResponse(
                     GetResponse.ResponseStatus.NotFound,
                     null);


        }
    }

}