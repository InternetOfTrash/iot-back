using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBreeze;


namespace iot_backend
{
    class Service : IDisposable
    {
        private static Service service = null;
        DBreezeEngine engine = null;
        private async void InitDB()
        {
            if (engine == null)
                engine = new DBreezeEngine(@"C:\temp\DBR1");
        }

        public void SetFillLevel(ContainerLevel cl)
        {
            if (cl.FillLevel > 100 || cl.FillLevel < 0)
            {
                throw new ArgumentException();
            }
            using (var tran = engine.GetTransaction())
            {
                tran.Insert<int, int>("containers", cl.ID, cl.FillLevel);
                tran.Commit();
            }

        }

        public int GetFillLevel(int id)
        {
            using (var transaction = engine.GetTransaction())
            {
                var row = transaction.Select<int, int>("containers", id);
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
    }
}