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
    public class PreviewPage
    {
        private IWebDriver driver;

        /// <summary>
        /// The constructor with PageFactory and driver
        /// </summary>
        /// <param name="driver">Google Chrome WebDriver</param>
        public PreviewPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        //:::Buttons:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        /// <summary>
        /// Button "Edit"
        /// </summary>
        [FindsBy(How = How.ClassName, Using = "icon-edit")]
        public IWebElement buttonEdit;

        /// <summary>
        /// Button "Send for approval"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-click=\"vm.sendToApproval()\"]")]
        public IWebElement buttonSendForApproval;

        /// <summary>
        /// Button "Send for approval" on cofirmation dialog
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-click=\"vm.sendToApprovalConfirmed()\"]")]
        public IWebElement buttonSendForApprovalConfirm;

        /// <summary>
        /// Button "Price package"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "button[ng-click*=\"package_pricing\"]:enabled")]
        public IWebElement buttonPricePackage;

        /// <summary>
        /// Button "Approve" on Preview page
        /// </summary>
        [FindsBy(How = How.ClassName, Using = "icon-check-sign")]
        public IWebElement buttonApprove;

        /// <summary>
        /// Button "Approve" on the confirmation dialog
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".btn.btn-default.btn-success")]
        public IWebElement buttonApproveOnConfirmationDialog;

        /// <summary>
        /// Button "Next"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".icon-arrow-right.pull-right")]
        public IWebElement buttonNext;

        //:::Drop-Down:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        /// <summary>
        /// Drop-down "Select approver"
        /// </summary>
        [FindsBy(How = How.Id, Using = "selectApprover")]
        public IWebElement dropdownSelectApprover;

        //:::Text:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        [FindsBy(How = How.CssSelector, Using = "[ng-bind=\"vm.Package.StatusName\"]")]
        public IWebElement packageStatus;

    }
}
