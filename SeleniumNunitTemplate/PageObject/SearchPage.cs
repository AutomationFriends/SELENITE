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
    public class SearchPage
    {
        private IWebDriver driver;

        /// <summary>
        /// The constructor with PageFactory and driver
        /// </summary>
        /// <param name="driver">Google Chrome WebDriver</param>
        public SearchPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        //:::Buttons:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        /// <summary>
        /// Button Next
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".icon-arrow-right.pull-right")]
        public IWebElement buttonNext;

        /// <summary>
        /// Button Advance Search
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".btn-group :nth-child(2)")]
        public IWebElement buttonAdvanceSearch;

        /// <summary>
        /// Button Search
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-click=\"vm.SearchPackages()\"]")]
        public IWebElement buttonSearch;

        /// <summary>
        /// View package button 
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".ng-scope .nowrap.action-column .btn.btn-sm.btn-default")]
        public IList<IWebElement> buttonsPackageView;


    }


}
