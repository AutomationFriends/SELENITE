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

public class ApprovePage
    {
        private IWebDriver driver;
        public ApprovePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Search field 
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".form-control.ng-pristine.ng-untouched.ng-valid.ng-valid-maxlength[placeholder=\"Search by package Id or Name...\"]")]
        public IWebElement fieldSearch;

        /// <summary>
        /// View package button 
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[uib-tooltip=\"View/Edit package\"]")]
        public IWebElement buttonViewPackage;

        //:::Text:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        [FindsBy(How = How.CssSelector, Using = "[ng-bind=\"vm.Package.StatusName\"]")]
        public IWebElement packageStatus;
        
    }

}
