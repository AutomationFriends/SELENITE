using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using Scania.Selenium.Support.SelActions;
using System.Threading;

namespace SeleniumNunitTemplate
{
    /// <summary>
    /// This is a PageFactory class which contains locators for elements on the page -Create Page- 
    /// </summary>
    public class DescriptionPage : SelActions
    {

        /// <summary>
        /// The constructor with PageFactory and driver
        /// </summary>
        /// <param name="driver">WebDriver</param>
        public DescriptionPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        //:::Page Title:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// The page title
        /// </summary>
        public string createPackageTitle = "CreatePackage - Fixed Price Repair";

        //:::INPUT FIELDS::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        /// <summary>
        /// Field "VARIANT DESCRIPTION" 
        /// </summary>
        [FindsBy(How = How.Id, Using = "pkgVarients")]
        public IWebElement fieldVariantDescription;
        
        /// <summary>
        /// Field "REASON FOR PACKAGE" 
        /// </summary>
        [FindsBy(How = How.Id, Using = "pkgReason")]
        public IWebElement fieldReasonForPackage;
        
        /// <summary>
        /// Field "PACKAGE NAME" 
        /// </summary>
        [FindsBy(How = How.Id, Using = "pkgName")]
        public IWebElement fieldPackageName;

        /// <summary>
        /// Field "PACKAGE DESCRIPTION" 
        /// </summary>
        [FindsBy(How = How.Id, Using = "pkgDescription")]
        public IWebElement fieldPackageDescription;

        /// <summary>
        /// Field "CODE" 
        /// </summary>
        [FindsBy(How = How.Id, Using = "pkgCode")]
        public IWebElement fieldCode;

        /// <summary>
        /// Field "WORKORDER TEXT" 
        /// </summary>
        [FindsBy(How = How.Id, Using = "pkgWorkOrderText")]
        public IWebElement fieldWorkorderText;

        /// <summary>
        /// Field "INVOICE TEXT" 
        /// </summary>
        [FindsBy(How = How.Id, Using = "pkgInvoiceText")]
        public IWebElement fieldInvoiceText;

        //:::Drop-Down::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// List "MAIN GROUP" 
        /// </summary>
        [FindsBy(How = How.Id, Using = "pkgMainGroup")]
        public IWebElement listMainGroup;

        /// <summary>
        /// List "SUB GROUP" 
        /// </summary>
        [FindsBy(How = How.Id, Using = "pkgSubGroup")]
        public IWebElement listSubGroup;

        //:::Button::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// Button "Save"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".icon-save")]
        public IWebElement buttonSave;

        /// <summary>
        /// Button "Next"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".icon-arrow-right.pull-right")]
        public IWebElement buttonNext;

        /// <summary>
        /// Button "2 Content"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-click*=\"package_content\"]")]
        public IWebElement buttonContent;

        /// <summary>
        /// TOOLTIP "Svenska"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[uib-tooltip=\"Svenska\"]")]
        public IWebElement tooltipSvenska;

        //:::Message:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// Alert message e.g "Success! Package has been saved"
        /// </summary>
        [FindsBy(How = How.ClassName, Using = "toast-message")]
        public IWebElement alertMessage;

        //:::Lists:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        
        /// <summary>
        /// The list with "VEHICLE TYPE" 
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".checkbox.ng-scope label")]
        public IList<IWebElement> listVehicleType;

        //:::Complete functions:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        
        /// <summary>
        /// Fill out description page
        /// </summary>
        public void ToFillOutDescription(string mainGroup, string subGroup, string vehicleType, string variantDescription, string reasonForPackage, string packageName, string packageDescription, string workorderText, string invoiceText)
        {

            // Select - MAIN GROUP
            selectFromDropDownByText(mainGroup, listMainGroup);

            // Select - SUB GROUP
            selectFromDropDownByText(subGroup, listSubGroup);

            // Select - VEHICLE TYPE
            IWebElement searchableVehicleType = searchElementByText(vehicleType, listVehicleType);
            searchableVehicleType.Click();

            // Specify - VARIANT DESCRIPTION
            writeTextToField(variantDescription, fieldVariantDescription);
            
            // Specify - REASON FOR PACKAGE
            writeTextToField(reasonForPackage, fieldReasonForPackage);

            // ENGLISH (TOOLTIP "English")

            // Specify - PACKAGE NAME 
            writeTextToField(packageName, fieldPackageName);

            // Specify - PACKAGE DESCRIPTION
            writeTextToField(packageDescription, fieldPackageDescription);

            // Specify - WORKORDER TEXT
            writeTextToField(workorderText, fieldWorkorderText);

            // Specify - INVOICE TEXT
            writeTextToField(invoiceText, fieldInvoiceText);

            // SWEDISH (TOOLTIP "English")

            tooltipSvenska.Click();

            // Specify - PACKAGE NAME 
            writeTextToField(packageName, fieldPackageName);

            // Specify - PACKAGE DESCRIPTION
            writeTextToField(packageDescription, fieldPackageDescription);

            // Specify - WORKORDER TEXT
            writeTextToField(workorderText, fieldWorkorderText);

            // Specify - INVOICE TEXT
            writeTextToField(invoiceText, fieldInvoiceText);


        }

    }
    
}
