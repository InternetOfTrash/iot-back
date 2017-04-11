using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTBackEnd
{
    public class Server
    {
        public Server()
        {
            //Startup server
            var endpoint = "http://*:11001/";
            using (WebApp.Start<Startup>(endpoint))
            {
                System.Diagnostics.Process.Start("Http://localhost:11001/swagger");
                //Server shows where it is hosted and waits for input
                //Log.Information("Server @:" + endpoint);
                //Log.Information("Press enter to close server");
                Console.ReadLine();
            }
        }
    }
}
