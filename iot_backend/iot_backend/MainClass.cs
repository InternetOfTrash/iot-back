using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot_backend
{
    public class MainClass
    {
        public static void Main()
        {
            //Console.WriteLine(ReverseGeo.GetAddressFromCoords("51.363741", "6.159683"));
            Server servor = new Server();
            // Service s = new iot_backend.Service();
            Service s = Service.GetInstance();
//s.SetFillLevel(1, 10);
           // Console.WriteLine("The fill level of container 1 is: " + s.GetFillLevel(1));

            while (true)
            {

            }
        }
    }
}
