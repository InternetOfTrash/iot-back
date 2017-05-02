using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBreeze;
using MongoDB.Bson;
using MongoDB.Driver;
using DBreeze.DataTypes;
using DBreeze.Exceptions;

namespace iot_backend
{
    class Service : IDisposable
    {
        private static Service service = null;
        DBreezeEngine engine = null;

        private async void InitDB()
        {
            if (engine == null)
                engine = new DBreezeEngine(@"/DBR1");

            //using (var tran = engine.GetTransaction())
            //{
            //    Container container1 = new Container("prototype_container1", (float)51.353351, (float)6.153967);
            //    Container container2 = new Container("prototype_container2", (float)51.359951, (float)6.159428);
            //    Container container3 = new Container("prototype_container3", (float)51.363751, (float)6.159725);
            //    Container container4 = new Container("prototype_container4", (float)51.362478, (float)6.168780);
            //    Container container5 = new Container("prototype_container5", (float)51.359316, (float)6.173866);
            //    Container container6 = new Container("prototype_container6", (float)51.357119, (float)6.183567);
            //    Container container7 = new Container("prototype_container7", (float)51.359873, (float)6.183738);
            //    Container container8 = new Container("prototype_container8", (float)51.362131, (float)6.181056);
            //    Container container9 = new Container("prototype_container9", (float)51.362955, (float)6.182687);
            //    Container container10 = new Container("prototype_container10", (float)51.364475, (float)6.178567);

            //    tran.Insert<string, DbXML<Container>>("containers", container1.ID, container1);
            //    tran.Insert<string, DbXML<Container>>("containers", container2.ID, container2);
            //    tran.Insert<string, DbXML<Container>>("containers", container3.ID, container3);
            //    tran.Insert<string, DbXML<Container>>("containers", container4.ID, container4);
            //    tran.Insert<string, DbXML<Container>>("containers", container5.ID, container5);
            //    tran.Insert<string, DbXML<Container>>("containers", container6.ID, container6);
            //    tran.Insert<string, DbXML<Container>>("containers", container7.ID, container7);
            //    tran.Insert<string, DbXML<Container>>("containers", container8.ID, container8);
            //    tran.Insert<string, DbXML<Container>>("containers", container9.ID, container9);
            //    tran.Insert<string, DbXML<Container>>("containers", container10.ID, container10);
            //    tran.Commit();
            //}
        }

        public List<Entry> GetHistory(string id)
        {
            List<Entry> historyList = new List<Entry>();
            Dictionary<int, DbXML<Entry>> dictionary = null;
            using (var transaction = engine.GetTransaction())
            {
                dictionary = transaction.SelectDictionary<int, DbXML<Entry>>("history");
            }
            foreach (KeyValuePair<int, DbXML<Entry>> pair in dictionary)
            {
                if (pair.Value.Get.ID == id)
                {
                    historyList.Add(pair.Value.Get);
                }
            }
            return historyList;
        }

        public void SetFillLevel(ContainerLevel cl)
        {
            if (cl.FillLevel > 100 || cl.FillLevel < 0)
            {
                throw new ArgumentException();
            }
            string ID = cl.ID;
            Container container = GetContainer(ID);
            container.FillLevel = cl.FillLevel;
            container.LastUpdated = DateTime.Now;
            int fillLevel = GetFillLevel(ID);
            DateTime dateTime = GetDateTime(ID);
            Entry nEntry = new Entry(ID, dateTime, fillLevel);
            bool success = false;
            using (var tran = engine.GetTransaction())
            {

                try
                {
                    tran.Insert<string, DbXML<Container>>("containers", ID, container);

                    tran.Commit();
                    success = true;
                }
                catch (DBreezeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            if (success == true)
            {
                ulong count = 0;
                using (var tran = engine.GetTransaction())
                {
                    count = tran.Count("history");
                }
                using (var tran = engine.GetTransaction())
                {
                    tran.Insert<int,DbXML<Entry>>("history", (int)count, nEntry);
                    tran.Commit();
                }
            }

        }

        public int GetFillLevel(string id)
        {
            using (var transaction = engine.GetTransaction())
            {
                var row = transaction.Select<string, DbXML<Container>>("containers", id);
                int res;
                if (row.Exists)
                {
                    res = row.Value.Get.FillLevel;
                    //res = row.Value.GetFillLevel();
                    return res;
                }
                else
                {
                    return -1;
                }
            }
        }

        public DateTime GetDateTime(string id)
        {
            using (var transaction = engine.GetTransaction())
            {
                var row = transaction.Select<string, DbXML<Container>>("containers", id);
                DateTime res;
                if (row.Exists)
                {
                    res = row.Value.Get.LastUpdated;
                    //res = row.Value.GetLastUpdated();
                    return res;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public Container GetContainer(string id)
        {
            using (var transaction = engine.GetTransaction())
            {
                var row = transaction.Select<string, DbXML<Container>>("containers", id);
                Container res;
                if (row.Exists)
                {
                    res = row.Value.Get;
                    //res = row.Value;
                    return res;
                }
                else
                {
                    return null;
                }
            }
        }


        public List<ContainerLevel> GetFillLevels()
        {
            List<ContainerLevel> cLevelList = new List<ContainerLevel>();
            Dictionary<string, DbXML<Container>> dictionary = null;
            using (var transaction = engine.GetTransaction())
            {
                dictionary = transaction.SelectDictionary<string,DbXML<Container>>("containers");
            }
            foreach(KeyValuePair<string,DbXML<Container>> pair in dictionary)
            {
                cLevelList.Add(new ContainerLevel(pair.Key, pair.Value.Get.FillLevel));
            }
            return cLevelList;
        }
        private Service()
        {
            InitDB();
        }

        public static Service GetInstance()
        {
            if (service == null)
            {
                service = new iot_backend.Service();
            }
            return service;
        }

        public void Dispose()
        {
            if (engine != null)
                engine.Dispose();
        }

        private void ClearContainers()
        {
            using (var tran = engine.GetTransaction())
            {
                tran.RemoveAllKeys("containers",true);
                tran.Commit();
            }
        }
    }
}