using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

using OpenQA.Selenium.Remote; // for grid

using System;
using System.Threading;

namespace ScaniaNUnitProject
{

    [TestFixture]

    public class TestGrid
    {


        IWebDriver driver;
        public string baseURL;
        public const int TimeOut = 30;

        [SetUp]
        public void Setup()
        {
            // Test comments 1

            //Create a new Firefox profile
            //var firefoxProfile = new FirefoxProfile();
            //Create a new proxy object
            //var proxy = new Proxy();
            //Set the http proxy value, host and port.
            //proxy.HttpProxy = "PROXYSESO.SCANIA.COM:8080";
            //We then add this proxt setting to the Firefox profile we created
            //firefoxProfile.SetProxyPreferences(proxy);
            //Then create a new Firefox Driver passing in the profile we created
            //WebDriver we open a Firefox using this profile now
            //driver = new FirefoxDriver(firefoxProfile); 


            //driver = new FirefoxDriver();


            //DesiredCapabilities capabilities = new DesiredCapabilities();
            //capabilities = DesiredCapabilities.Chrome();

            //FirefoxProfile profile = new FirefoxProfile();
            DesiredCapabilities capabilities = new DesiredCapabilities();
            Proxy proxy = new Proxy();
            proxy.IsAutoDetect = true;
            //profile.SetProxyPreferences(proxy);
            
            capabilities = DesiredCapabilities.Chrome();


            capabilities.SetCapability("seleniumProtocol", "WebDriver");
            capabilities.SetCapability(CapabilityType.BrowserName, "chrome");
            //capabilities.SetCapability(CapabilityType.Version, "31.0");
            capabilities.SetCapability(CapabilityType.Platform, "LINUX");
            //capabilities.SetCapability(CapabilityType.Proxy, proxy);
   

            //{seleniumProtocol=Selenium, browserName=*googlechrome, maxInstances=1, platform=LINUX}


            //capabilities.SetCapability("browser", "Firefox");
            //capabilities.SetCapability("browser_version", "31.0");
            //capabilities.SetCapability("os", "Windows");
            //capabilities.SetCapability("os_version", "7");
            //capabilities.SetCapability("resolution", "1024x768");
            //capabilities.SetCapability("build111", "version111");
            //capabilities.SetCapability("project111", "newintropage111");



            //driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capabilities);

            //driver = new FirefoxDriver(profile);
            driver = new RemoteWebDriver(new Uri("http://192.168.59.103:4444/wd/hub"), capabilities);
        }
/*
        [Test]
        public void TestGrid1()
        {
            //driver.Manage().Window.Maximize();
            baseURL = "http://www.scania.se/";
            driver.Navigate().GoToUrl(baseURL);
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30000));
            Thread.Sleep(5000);
            driver.FindElement(By.ClassName("search")).Click();
            Thread.Sleep(2000);

        }
*/
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

    }
}