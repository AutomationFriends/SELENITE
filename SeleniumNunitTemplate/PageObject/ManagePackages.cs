using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace SeleniumNunitTemplate
{
    /// <summary>
    /// This is a PageFactory class which contains locators for elements on the page -Manage package Page- 
    /// </summary>
    public class ManagePackages
    {
        private IWebDriver driver;

        /// <summary>
        /// The constructor with PageFactory and driver
        /// </summary>
        /// <param name="driver">Internet Explorer WebDriver</param>
        public ManagePackages(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        //:::Page Title:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        
        /// <summary>
        /// The page title
        /// </summary>
        public string mainPageTitle = "Manage packages - Fixed Price Repair";

        //:::Fields:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        
        /// <summary>
        /// Search field 
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".form-control.ng-pristine.ng-untouched.ng-valid.ng-valid-maxlength[placeholder=\"Search by package Id or Name...\"]")]
        public IWebElement fieldSearch;
        
        //:::Buttons:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        /// <summary>
        /// View package button 
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[uib-tooltip=\"View/Edit package\"]")]
        public IWebElement buttonViewPackage;

        //:::Elements:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        /// <summary>
        /// The first package in the search
        /// </summary>
        [FindsBy(How = How.ClassName, Using = "ui-select-highlight")]
        public IWebElement SearchablePackage;


        [FindsBy(How = How.CssSelector, Using = ".ng-scope .nowrap.action-column .btn.btn-sm.btn-default")]
        public IList<IWebElement> buttonsPackageView;

    }
}
