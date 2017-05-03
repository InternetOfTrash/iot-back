using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace iot_backend
{
    public class ReverseGeo
    {
        private static ReverseGeo Instance;
        static string baseUri = "http://maps.googleapis.com/maps/api/" +
                          "geocode/xml?latlng={0},{1}&sensor=false";

        private ReverseGeo()
        {

        }

        public static ReverseGeo GetInstance()
        {
            if (Instance == null)
                Instance = new ReverseGeo();

            return Instance;
        }

        public string GetAddressFromCoords(string lat, string lng)
        {
            lat = lat.Replace(",", ".");
            lng = lng.Replace(",", ".");
            string requestUri = string.Format(baseUri, lat, lng);

            using (var client = new WebClient())
            {
                client.Headers.Add("Accept-Language", " en-US");
                client.Headers.Add("Accept", " text/html, application/xhtml+xml, */*");
                client.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");

                try
                {
                    string geocodeInfo = client.DownloadString(requestUri);
                
                var xmlElm = XElement.Parse(geocodeInfo);

                var status = (from elm in xmlElm.Descendants()
                              where elm.Name == "status"
                              select elm).FirstOrDefault();
                if (status.Value.ToLower() == "ok")
                {
                    var res = (from elm in xmlElm.Descendants()
                               where elm.Name == "formatted_address"
                               select elm).FirstOrDefault();
                    return res.Value;
                }
                else
                {
                    return "";
                }

                    // return geocodeInfo[1];
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return "";
        }

   

    }
}
