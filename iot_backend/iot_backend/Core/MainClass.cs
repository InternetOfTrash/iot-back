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
            Server server = new Server();
            Service s = Service.GetInstance();
            while (true)
            {
            }
        }
    }
}
