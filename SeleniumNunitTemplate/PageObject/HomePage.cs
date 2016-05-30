using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using Scania.Selenium.Support.SelActions;

namespace SeleniumNunitTemplate
{
    /// <summary>
    /// This is a PageFactory class which contains locators for elements on the page -Home Page- 
    /// </summary>
    public class HomePage : SelActions
    {
        private IWebDriver driver;

        /// <summary>
        /// The constructor with PageFactory and driver
        /// </summary>
        /// <param name="driver">Internet Explorer WebDriver</param>
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        //:::Page Title:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// The page title
        /// </summary>
        public string mainPageTitle = "Home page - Fixed Price Repair";


        //:::Menu:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// Menu "Packages" 
        /// </summary>
        [FindsBy(How = How.LinkText, Using = "Packages")]
        public IWebElement menuPackages;

        /// <summary>
        /// Menu "Create" 
        /// </summary>
        [FindsBy(How = How.LinkText, Using = "Create")]
        public IWebElement menuCreate;

        [FindsBy(How = How.Id, Using = "btnLoggedUser")]
        public IWebElement buttonLoggedUser;

        [FindsBy(How = How.ClassName, Using = "icon-signout")]
        public IWebElement buttonSwitchUser;




        //:::Big Icons:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// Big Icons 
        /// </summary>
        [FindsBy(How = How.ClassName, Using = "panel-heading")]
        public IList<IWebElement> allIcons;


        public void ClickButtonImport()
        {
            IWebElement BigIcon_Import = searchElementByText("Import", allIcons);
            BigIcon_Import.Click();
        }


    }
}
