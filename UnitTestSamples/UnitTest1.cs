using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestLooper;
using System.Collections.Generic;
namespace UnitTestSamples
{
    [TestLooper]
    public class UnitTest1
    {
        public static List<String> strings = new List<String>();
        private TestContext testContextInstance;

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
        [ClassInitialize()]
        public static void Init(TestContext x)
        {
            strings.Add("A");
            strings.Add("B");
            strings.Add("C");
            strings.Add("D");

        }

        [TestInitialize()]
        public void TestInit()
        {
            if (!TestContext.Properties.Contains(TestLooperAttribute.LOOPER_CONTEXT_PROPERTY))
                testContextInstance.Properties.Add(TestLooperAttribute.LOOPER_CONTEXT_PROPERTY, strings);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "DataDriven1.csv", "DataDriven1#csv", DataAccessMethod.Sequential)]
        [DeploymentItem("DataDriven1.csv")]
        public void TestMethodStrings(string s)
            
        {
            int value1 = Convert.ToInt32(TestContext.DataRow["Col1"]); ;
            TestContext.WriteLine(String.Format("{0}:{1}", s, value1));
        }
    }
}
