using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTBackEnd
{
    public class ContainerLevel
    {
        public int ID { get; set; }
        public int FillLevel { get; set; }

        public ContainerLevel(int ID, int fillLevel)
        {
            this.ID = ID;
            this.FillLevel = fillLevel;
        }

    }
}
