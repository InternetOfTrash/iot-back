using System;

namespace iot_backend
{
    public class MetaData
    {
        public string time = "";
        public double frequency = 0.0;
        public string modulation = "";
        public string data_rate = "";
        //public int bit_rate = 0;
        public string coding_rate = "";
        public Gateways[] gateways;
    }
}