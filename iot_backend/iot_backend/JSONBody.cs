using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot_backend
{
    public class JSONBody
    {
        public string app_id = "";
        public string dev_id = "";
        public string hardware_serial = "";
        public int port = 0;
        public int counter = 0;
        //public bool is_retry = false;
        //public bool confirmed = false;
        public string payload_raw = "";
        public PayloadFields payload_fields;
        public MetaData metadata;
        public string downlink_url = "";
    }
}
