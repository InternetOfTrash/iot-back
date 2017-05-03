using Microsoft.VisualStudio.TestTools.UnitTesting;
using iot_backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;

namespace iot_backend.Controllers.Tests
{
    [TestClass()]
    public class FillLevelControllerTests
    {
        private Server server;
        [TestMethod()]
        public void InitTest()
        {
            server = new Server();
        }

        private List<ContainerLevel> GetTestLevels()
        {
            var clList = new List<ContainerLevel>();
            clList.Add(new ContainerLevel("1", 10));
            clList.Add(new ContainerLevel("2", 20));
            clList.Add(new ContainerLevel("3", 30));
            clList.Add(new ContainerLevel("4", 40));
            clList.Add(new ContainerLevel("5", 50));
            return clList;
        }

        [TestMethod()]
        public void GetTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTest1()
        {
            var testLevels = GetTestLevels();
            var controller = new FillLevelController();

            var result = controller.Get() as List<ContainerLevel>;
            Assert.AreEqual(testLevels.Count, result.Count);
        }

        [TestMethod()]
        public void PostTest()
        {
            Assert.Fail();
        }
    }
}