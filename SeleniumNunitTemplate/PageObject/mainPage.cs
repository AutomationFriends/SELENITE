using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace TemplateSeleniumTest
{
    /// <summary>
    /// This is a PageFactory class which contains locators for elements on the page -mainPage- 
    /// </summary>
    public class mainPage
    {
        private IWebDriver driver;

        /// <summary>
        /// The constructor with PageFactory and driver
        /// </summary>
        /// <param name="driver">Internet Explorer WebDriver</param>
        public mainPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        //:::Page Title:::

        /// <summary>
        /// The page title
        /// </summary>
        public string mainPageTitle = "...";

        /// <summary>
        /// Ducument Title
        /// </summary>
        [FindsBy(How = How.Id, Using = "documentTitle")]
        public IWebElement textDucumentTitle;

        //:::Fields:::

        /// <summary>
        /// Field
        /// </summary>
        [FindsBy(How = How.Id, Using = "...")]
        public IWebElement field;


    }
}
