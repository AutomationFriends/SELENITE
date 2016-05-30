using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Text.RegularExpressions;
using Scania.Selenium.Support.SelActions;

namespace SeleniumNunitTemplate
{
    /// <summary>
    /// This is a PageFactory class which contains locators for elements on the page -Conten Page- 
    /// </summary>
    public class ContentPage : SelActions
    {
        private IWebDriver driver;

        /// <summary>
        /// The constructor with PageFactory and driver
        /// </summary>
        public ContentPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        
        
        //:::DROP-DOWN::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        
        /// <summary>
        /// All drop-down lists on the page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".ui-select-placeholder.text-muted.ng-binding")]
        public IList<IWebElement> allDropDownLists;

        /// <summary>
        /// Search element by text in drop-down that will appear when you click on Parts/Labour/Sundries  
        /// </summary>
        /// <returns></returns>
        public IWebElement searchElementByTextInDropDown(string elementHasText, IList<IWebElement> allElements)
        {
            IWebElement myElement = null;

            foreach (IWebElement element in allElements)
            {

                if (element.Text.Contains(elementHasText))
                {
                    myElement = element;
                }
            }

            return myElement;
        }

        /// <summary>
        /// Parts drop-down list 
        /// </summary>
        public void dropDownSelectPart(string partToSelect)
        {
                    driver.FindElement(By.Name("partSelect")).Click();
                    Thread.Sleep(500);
                    driver.FindElement(By.CssSelector("input[placeholder=\"Select a part...\"]")).SendKeys(partToSelect);

                    IList<IWebElement> dropDownList = driver.FindElements(By.CssSelector(".row.ng-scope"));

                    IWebElement searchablePart = searchElementByTextInDropDown(partToSelect, dropDownList);

                    WaitForElement(searchablePart);
                    
                    searchablePart.Click();

        }

        /// <summary>
        /// Labour drop-down list
        /// </summary>
        public void dropDownSelectLabour(string labourToSelect)
        {

                    driver.FindElement(By.Name("stdTimeSelect")).Click();
                    Thread.Sleep(500);
                    driver.FindElement(By.CssSelector("input[placeholder=\"Select a labour...\"]")).SendKeys(labourToSelect);

                    IList<IWebElement> dropDownList = driver.FindElements(By.CssSelector(".row.ng-scope"));

                    IWebElement searchableLabour = searchElementByTextInDropDown(labourToSelect, dropDownList);
                    searchableLabour.Click();
                     
        }

        /// <summary>
        /// Sundries drop-down list
        /// </summary>
        public void dropDownSelectSundries(string sundriesToSelect)
        {

                    driver.FindElement(By.Name("sundrySelect")).Click();
                    Thread.Sleep(500);
                    driver.FindElement(By.CssSelector("input[placeholder=\"Select a sundry...\"]")).SendKeys(sundriesToSelect);

                    IList<IWebElement> dropDownList = driver.FindElements(By.CssSelector(".row.ng-scope"));

                    IWebElement searchableSundries = searchElementByTextInDropDown(sundriesToSelect, dropDownList);
                    searchableSundries.Click();

        }

        
        
        //:::BUTTONS::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
         
        /// <summary>
        /// Button Add Parts
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".btn.btn-success.btn-add.form-control.buttonAddPart")]
        public IWebElement buttonAddParts;

        /// <summary>
        /// Button Add Labour
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".btn.btn-success.btn-add.form-control.buttonAddStandardTime")]
        public IWebElement buttonAddLabour;

        /// <summary>
        /// Button Add Sundries
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".btn.btn-success.btn-add.form-control.buttonAddSundry")]
        public IWebElement buttonAddSundries;

        /// <summary>
        /// Button Save
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".icon-save")]
        public IWebElement buttonSave;

        
        
        //:::CHECKBOXES:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        /// <summary>
        /// Checkbox "MAIN PART" 
        /// </summary>
        [FindsBy(How = How.Name, Using = "mainPart")]
        public IWebElement checkbox_MAIN_PART;

        

        //:::TEXT ELEMENTS::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        
        /// <summary>
        /// Header with the package Id
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".page-header.text-center .ng-binding")]
        public IWebElement headerWithPackageId;



        //:::MESSAGES:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        
        /// <summary>
        /// Alert message e.g "Success! Package has been saved"
        /// </summary>
        [FindsBy(How = How.ClassName, Using = "toast-message")]
        public IWebElement alertMessage;

        

        //:::INPUT FIELDS:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        
        [FindsBy(How = How.CssSelector, Using = "[ng-model=\"part.Quantity\"]")]
        public IWebElement fieldQty;


        
        //:::MISCELLANEOUS FUNCTIONS:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        /// <summary>
        /// Wait that the page with a specified number will be active
        /// </summary>
        public void waitForActivePage(string pageNumber)
        {
            IWebElement pageNumberButton = driver.FindElement(By.ClassName("active"));

            for (int i = 0; i < 10; i++)
            {
                if (pageNumberButton.Text.ToString() == pageNumber)
                {
                    break;
                }
                Thread.Sleep(5);
            }
        }

        /// <summary>
        /// Choose Part, Labour and Sundry from the drop-down lists  
        /// </summary>
        public void ChoosePartLabourAndSundry(string part, string labour, string sundry)
        {

            // Select a labour
            dropDownSelectLabour(labour);
            buttonAddLabour.Click();

            // Select the a part
            dropDownSelectPart(part);
            buttonAddParts.Click();

            // Select a sundry
            dropDownSelectSundries(sundry);
            buttonAddSundries.Click();
        }

        /// <summary>
        /// Function to get package Id
        /// </summary>
        public string GetPackageId()
        {
            string packageId = headerWithPackageId.Text.ToString();

            packageId = packageId.Substring(packageId.IndexOf("("));
            string newPackageId = packageId.Trim(')', '(');

            return newPackageId;

        }


    }
}
