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
    public class BaseSetUp : SelActions
    {
        /// <summary>
        /// GLOBAL VARIABLES
        /// </summary>
        public static string packageId { get; set; }

        /// <summary>
        /// The url to test application
        /// </summary>
        public string URL = Properties.Settings.Default.testURL;

        public LoginPage LoginPage;
        public HomePage HomePage;
        public DescriptionPage DescriptionPage;
        public ManagePackages ManagePackages;
        public ContentPage ContentPage;
        public PreviewPage PreviewPage;
        public SearchPage SearchPage;
        public PackagesMenu PackagesMenu;
        public PricingPage PricingPage;
        public CleanData CleanData;
        public ApprovePage ApprovePage;
        public ImportPage ImportPage;

        /// <summary>
        /// Starts before each test and 
        /// contains drivers, urls, and needed instances
        /// </summary>
        [SetUp]
        public void Setup()
        {

            Driver getDriver = new Driver();
            driver = getDriver.driverChrome();

            LoginPage = new LoginPage(driver);
            HomePage = new HomePage(driver);
            DescriptionPage = new DescriptionPage(driver);
            ManagePackages = new ManagePackages(driver);
            ContentPage = new ContentPage(driver);
            PreviewPage = new PreviewPage(driver);
            SearchPage = new SearchPage(driver);
            PackagesMenu = new PackagesMenu(driver);
            PricingPage = new PricingPage(driver);
            CleanData = new CleanData();
            ApprovePage = new ApprovePage(driver);
            ImportPage = new ImportPage(driver);

            driver.Navigate().GoToUrl(URL);


        }

        /// <summary>
        /// Starts after each test and 
        /// contains methods to handle errors and close browser
        /// </summary>
        [TearDown]
        public void TearDown()
        {

//            ErrorHandle errorHandle = new ErrorHandle();
  //          errorHandle.takeScreenshot(driver);

            driver.Close();
            driver.Quit();

        }


    }
}
