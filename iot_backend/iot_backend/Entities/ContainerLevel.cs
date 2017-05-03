using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot_backend
{
    public class ContainerLevel
    {
        public string ID { get; set; }
        public int FillLevel { get; set; }

        public ContainerLevel(string ID, int fillLevel)
        {
            this.ID = ID;
            this.FillLevel = fillLevel;
        }

    }
}
