using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace SeleniumNunitTemplate
{

public class ImportPage
    {
        private IWebDriver driver;
        public ImportPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Name, Using = "mainGroup")]
        public IWebElement dropDownMainGroup;

        [FindsBy(How = How.Name, Using = "subGroup")]
        public IWebElement dropDownSubGroup;

        [FindsBy(How = How.Name, Using = "packageCode")]
        public IWebElement inputFieldPackageCode;

        [FindsBy(How = How.CssSelector, Using = "[type=\"submit\"]")]
        public IWebElement buttonSearch;

        
        

        
    }

}
