using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iot_backend
{
    public class Container
    {

        public Container()
        {
           
        }

        public Container(string ID, float latitude, float longitude)
        {
            this.ID = ID;
            this.latitude = latitude;
            this.longitude = longitude;
            this.fillLevel = 0;
            this.lastUpdated = DateTime.Now;
            this.prediction = DateTime.Now;
            prediction.AddDays(7);
        }


        private int fillLevel;

        public int FillLevel
        {
            get { return fillLevel; }
            set { fillLevel = value;}
        }

        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private float longitude;

        public float Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        private float latitude;

        public float Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        private DateTime lastUpdated;

        public DateTime LastUpdated
        {
            get { return lastUpdated; }
            set { lastUpdated = value; }
        }

        private DateTime prediction;

        public DateTime Prediction
        {
            get {
                //UpdatePrediction();
                return prediction; }
            set { prediction = value; }
        }

        public void UpdatePrediction()
        {
            List<Entry> hist = Service.GetInstance().GetHistory(id);
            if(hist.Count == 0)
            {
                prediction = LevelPredictor.GetExpectedFillDate(LastUpdated, FillLevel);
            }
            else
            {
                Entry e = hist[hist.Count - 1];
                prediction = LevelPredictor.GetExpectedFillDate(e.TimeStamp, e.FillLevel, LastUpdated, FillLevel);
            }
            
        }

    }
}
