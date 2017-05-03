using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot_backend
{
    public class ReverseGeo
    {
        private ReverseGeo Instance;
        private ReverseGeo()
        {

        }

        public ReverseGeo GetInstance()
        {
            if (Instance == null)
                Instance = new ReverseGeo();

            return Instance;
        }
    }
}
