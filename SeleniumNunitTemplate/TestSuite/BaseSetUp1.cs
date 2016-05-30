using NUnit.Framework;
using System.Collections.Generic;
using Scania.Selenium.Support.SelActions;
using Scania.Selenium.Support.Driver;
using Scania.Selenium.Support.ErrorHandle;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using System;
using SeleniumNunitTemplate;
using System.Threading;

namespace SeleniumNunitTemplate
{
    /// <summary>
    /// This class contains general attributes [SetUp] and [Teardown] which uses all test cases
    /// </summary>
    [TestFixture]
    public class BaseSetUp1 : SelActions
    {
        /// <summary>
        /// The url to test application
        /// </summary>

        /// <summary>
        /// Starts before each test and 
        /// contains drivers, urls, and needed instances
        /// </summary>
        [SetUp]
        public void Setup()
        {

            Driver getDriver = new Driver();
            driver = getDriver.driverChrome();

            driver.Navigate().GoToUrl("http://s0539:7191/docarc/common/emxNavigator.jsp?appName=null");
            Thread.Sleep(2000);

            driver.FindElement(By.Name("login_name")).SendKeys("ssstestdc");
            driver.FindElement(By.Name("login_password")).SendKeys("3vligt");
            driver.FindElement(By.ClassName("btn")).Click();
            Thread.Sleep(5000);
            
            

        }

        /// <summary>
        /// Starts after each test and 
        /// contains methods to handle errors and close browser
        /// </summary>
        [TearDown]
        public void TearDown()
        {

            ErrorHandle errorHandle = new ErrorHandle();
            errorHandle.takeScreenshot(driver);

            driver.Close();
            driver.Quit();

        }
/*
        [Test]
        public void TestEmma()
        {

            Thread.Sleep(5000);
            
            
            IList<IWebElement> list = driver.FindElements(By.CssSelector(".menu-panel.global.add .menu-content :nth-child(1) :nth-child(2)"));
            Thread.Sleep(2000);
            IWebElement buttonCreateDocument = searchElementByText("Create Document...", list);
            

            driver.FindElement(By.CssSelector(".icon-button.add")).Click();
            //driver.FindElement(By.LinkText("Create Document...")).Click();

            

            //buttonCreateDocument.Click();


            driver.FindElement(By.CssSelector(".menu-panel.global.add .menu-content :first-child :nth-of-type(2)")).Click();
            Thread.Sleep(5000);

        }

*/

    }
}
