using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iot_backend;

namespace iot_backendTests
{
    /// <summary>
    /// Summary description for TestMailClient
    /// </summary>
    [TestClass]
    public class TestMailClient
    {
        public TestMailClient()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestIsValidEmail()
        {
            MailClient m = new MailClient();
            Assert.IsTrue(m.isValidEmail("email@domain.com"));
            Assert.IsTrue(m.isValidEmail("firstname.lastname@domain.com"));
            Assert.IsTrue(m.isValidEmail("email@subdomain.domain.com"));
            Assert.IsTrue(m.isValidEmail("firstname+lastname@domain.com"));
            Assert.IsTrue(m.isValidEmail("email@123.123.123.123"));
            Assert.IsTrue(m.isValidEmail("email@[123.123.123.123]"));
            //Assert.IsTrue(m.isValidEmail("“email”@domain.com"));
            Assert.IsTrue(m.isValidEmail("1234567890@domain.com"));
            Assert.IsTrue(m.isValidEmail("email@domain-one.com"));
            //Assert.IsTrue(m.isValidEmail("_______@domain.com"));
            Assert.IsTrue(m.isValidEmail("email@domain.name"));
            Assert.IsTrue(m.isValidEmail("email@domain.co.jp"));
            Assert.IsTrue(m.isValidEmail("firstname-lastname@domain.com"));

            Assert.IsFalse(m.isValidEmail("plainaddress"));
            Assert.IsFalse(m.isValidEmail("#@%^%#$@#$@#.com"));
            Assert.IsFalse(m.isValidEmail("@domain.com"));
            Assert.IsFalse(m.isValidEmail("Joe Smith <email@domain.com>"));
            Assert.IsFalse(m.isValidEmail("email.domain.com"));
            Assert.IsFalse(m.isValidEmail("email@domain@domain.com"));
            Assert.IsFalse(m.isValidEmail(".email@domain.com"));
            Assert.IsFalse(m.isValidEmail("email.@domain.com"));
            Assert.IsFalse(m.isValidEmail("email..email@domain.com"));
            Assert.IsFalse(m.isValidEmail("あいうえお@domain.com"));
            Assert.IsFalse(m.isValidEmail("email@domain.com (Joe Smith)"));
            Assert.IsFalse(m.isValidEmail("email@domain"));
            Assert.IsFalse(m.isValidEmail("email@-domain.com"));
            //Assert.IsFalse(m.isValidEmail("email@domain.web"));
            //Assert.IsFalse(m.isValidEmail("email@111.222.333.44444"));
            Assert.IsFalse(m.isValidEmail("email@domain..com"));
           
        }
    }
}
