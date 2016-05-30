using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Threading;

namespace SeleniumNunitTemplate
{
    /// <summary>
    /// This is a PageFactory class which contains locators for elements on the page -Manage package Page- 
    /// </summary>
    public class PackagesMenu
    {
        private IWebDriver driver;

        /// <summary>
        /// The constructor with PageFactory and driver
        /// </summary>
        /// <param name="driver">Google Chrome WebDriver</param>
        public PackagesMenu(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        //:::Buttons:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        /// <summary>
        /// Button "Manage" 
        /// </summary>
        [FindsBy(How = How.LinkText, Using = "Manage")]
        public IWebElement buttonManage;

        /// <summary>
        /// Button Advance Search
        /// </summary>
        [FindsBy(How = How.LinkText, Using = "Search")]
        public IWebElement buttonSearch;


    }


}
