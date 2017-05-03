using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot_backend
{
    public class Entry
    {

        public Entry()
        {

        }
        public Entry(string ID, DateTime dateTime, int fillLevel)
        {
            this.ID = ID;
            this.TimeStamp = dateTime;
            this.FillLevel = fillLevel;
        }

        private int fillLevel;

        public int FillLevel
        {
            get { return fillLevel; }
            set { fillLevel = value; }
        }

        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private DateTime dateTime;

        public DateTime TimeStamp
        {
            get { return dateTime; }
            set { dateTime = value; }
        }

    }
}
