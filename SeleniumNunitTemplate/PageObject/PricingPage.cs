using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Threading;
using Scania.Selenium.Support.SelActions;

namespace SeleniumNunitTemplate
{
    /// <summary>
    /// This is a PageFactory class which contains locators for elements on the page -Manage package Page- 
    /// </summary>
    public class PricingPage:SelActions
    {
        private IWebDriver driver;

        /// <summary>
        /// The constructor with PageFactory and driver
        /// </summary>
        /// <param name="driver">Google Chrome WebDriver</param>
        public PricingPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        //--- PAGE FACTORY OBJECTS --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- START --- ---//

        /// <summary>
        // The list with two buttons "Fixed price" and "Discount on items"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-model = \"pricingVM.showDiscount\"]")]
        public IList<IWebElement> listWithDiscountButtons;

        /// <summary>
        // The list with buttons on pricing page
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".btn.btn-primary")]
        public IList<IWebElement> listWithButtons;

        /// <summary>
        // Button open the list
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = ".icon-caret-right")]
        public IWebElement buttonOpenListWithParts;

        /// <summary>
        // The arrow to open the list with packags/labours/sandries
        /// </summary>
        [FindsBy(How = How.ClassName, Using = "icon-caret-right")]
        public IWebElement dropDownArrow;

            // --- PRICE OBJECTS --- START ---//

        /// <summary>
        // Field "QTY"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-repeat=\"part in value\"] td:nth-child(5)")]
        public IWebElement fieldQuantity;

        /// <summary>
        // Field "COST"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-repeat=\"part in value\"] td:nth-child(6)")]
        public IWebElement fieldCost;

        /// <summary>
        // Field "RETAIL PRICE"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-repeat=\"part in value\"] td:nth-child(8)")]
        public IWebElement fieldRetailPrice;

        /// <summary>
        // Field "SALES NET PRICE"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-repeat=\"part in value\"] td:nth-child(11)")]
        public IWebElement fieldSalesNetPrice;

        /// <summary>
        // Field "TOTAL COST"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-repeat=\"part in value\"] td:nth-child(14)")]
        public IWebElement fieldTotalCost;

        /// <summary>
        // Field "TOTAL GROSS"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-repeat=\"part in value\"] td:nth-child(13)")]
        public IWebElement fieldTotalGross;

        /// <summary>
        // Field "TOTAL NET"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-repeat=\"part in value\"] td:nth-child(12)")]
        public IWebElement fieldTotalNet;

        /// <summary>
        // Field "GROSS PROFIT amount"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-repeat=\"part in value\"] td:nth-child(15)")]
        public IWebElement fieldGrossProfitAmount;

        /// <summary>
        // Field "GROSS PROFIT percentage"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-repeat=\"part in value\"] td:nth-child(16)")]
        public IWebElement fieldGrossProfitPercentage;

        /// <summary>
        // Field "DISCOUNT %"
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "[ng-model=\"part.DiscountPercentage\"]")]
        public IWebElement fieldDiscountPercent;

            // --- PRICE OBJECTS --- END ---//

        //--- PAGE FACTORY OBJECTS --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- END --- ---//

        //--- EXTERNAL FUNCTIONS --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- START ---//

        /// <summary>
        /// Convert price fields to decimal
        /// </summary>
        public decimal ToDecimal(IWebElement field)
        {
            string stringField = field.Text.Remove(0, 2).Replace(".", ",").Replace("%", "");
            decimal convertedField = Convert.ToDecimal(stringField);
            return convertedField;
        }

        /// <summary>
        /// To Double
        /// </summary>
        public double ToDouble(IWebElement field)
        {
            Console.WriteLine(field.Text);
            string stringField = field.Text.Remove(0, 2).Replace(".", ",").Replace("%", "");
            double convertedField = Convert.ToDouble(stringField);
            return convertedField;
        }

        /// <summary>
        /// Set discount in percent
        /// </summary>
        public void ToSetDiscountPercentage(string discount)
        {
            fieldDiscountPercent.Clear();
            fieldDiscountPercent.SendKeys(discount);
        }
        
        /// <summary>
        /// Click on the market
        /// </summary>
        public void ToFindMarket(string marketName)
        {
            driver.FindElement(By.CssSelector(String.Format("[value='{0}']", marketName))).Click();
        }

        /// <summary>
        /// Click on button "Discount on itens"
        /// </summary>
        public void buttonDiscountOnItems()
        {
            IWebElement buttonDiscountOnItems = searchElementByText("Discount on items", listWithDiscountButtons);
            buttonDiscountOnItems.Click();
        }

        /// <summary>
        /// Click on button "Price for group"
        /// </summary>
        public void buttonPriceForGroup()
        {
            IWebElement buttonPriceForGroup = searchElementByText("Price for group", listWithButtons);
            buttonPriceForGroup.Click();
        }

        //--- EXTERNAL FUNCTIONS --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- END ---//
    }


}
