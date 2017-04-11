using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBreeze;
using MongoDB.Bson;
using MongoDB.Driver;

namespace iot_backend
{
    class Service : IDisposable
    {
        private static Service service = null;
        DBreezeEngine engine = null;
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        protected IMongoCollection<BsonDocument> collection;
        private async void InitDB()
        {
            if (engine == null)
                engine = new DBreezeEngine(@"/DBR1");

            ////_client = new MongoClient("mongodb://<dbuser>:<dbpassword>@ds137730.mlab.com:37730");
            ////_database = _client.GetDatabase("internet_of_trash");
            ////collection = _database.GetCollection<BsonDocument>("filllevels");
            ////var document = new BsonDocument
            ////{
            ////    { "_id", "1" },
            ////    { "filllevel", "55" }
            ////};
            ////collection.insert(document);

        }

        public void SetFillLevel(ContainerLevel cl)
        {
            if (cl.FillLevel > 100 || cl.FillLevel < 0)
            {
                throw new ArgumentException();
            }
            using (var tran = engine.GetTransaction())
            {
                tran.Insert<string, int>("containers", cl.ID, cl.FillLevel);
                
                tran.Commit();
            }

        }

        public int GetFillLevel(string id)
        {
            using (var transaction = engine.GetTransaction())
            {
                var row = transaction.Select<string, int>("containers", id);
                int res;
                if (row.Exists)
                {
                    res = row.Value;
                    return res;
                }
                else
                {
                    return -1;
                }
            }
        }

        public List<ContainerLevel> GetFillLevels()
        {
            List<ContainerLevel> cLevelList = new List<ContainerLevel>();
            Dictionary<string, int> dictionary = null;
            using (var transaction = engine.GetTransaction())
            {
                dictionary = transaction.SelectDictionary<string,int>("containers");
            }
            foreach(KeyValuePair<string,int> pair in dictionary)
            {
                cLevelList.Add(new ContainerLevel(pair.Key, pair.Value));
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