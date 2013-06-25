using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestLooper;
using OpenQA.Selenium;
using System.Collections.Generic;
namespace UnitTestSamples
{
    [TestLooper()]
    public class SeleniumTests
    {
        public static List<IWebDriver> Browsers = new List<IWebDriver>();
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
            OpenQA.Selenium.Firefox.FirefoxDriver fd = new OpenQA.Selenium.Firefox.FirefoxDriver();
            Browsers.Add(fd);
            OpenQA.Selenium.Chrome.ChromeDriver cd = new OpenQA.Selenium.Chrome.ChromeDriver();
            Browsers.Add(cd);

            
        }

        [TestInitialize()]
        public void TestInit()
        {
            if (!TestContext.Properties.Contains(TestLooperAttribute.LOOPER_CONTEXT_PROPERTY))
                testContextInstance.Properties.Add(TestLooperAttribute.LOOPER_CONTEXT_PROPERTY, Browsers);
        }

        [TestMethod]
        public void TestMethod1(IWebDriver driver)
        {

            driver.Url = "http://www.google.com";
        }
    }
}
