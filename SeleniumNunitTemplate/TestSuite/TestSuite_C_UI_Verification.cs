using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Scania.Selenium.Support.DataImport;
using Scania.Selenium.Support.Logging;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace SeleniumNunitTemplate
{


    /// <summary>
    /// This class contains tests and data sources
    /// </summary>
    [TestFixture]
    public class TestSuite_C_UI_Verification : BaseSetUp
    {

        public WriteToExcel WriteToExcel;

        /// <summary>
        /// Search a package -> Price package -> Choose MARKET -> Set a DISCOUNT -> Verify "SALES NET PRICE" calculation 
        /// </summary>
        [Test, Ignore("Ignore a test")]
        public void Test_C_0_FetchPackagePrices()
        {

            // TESTparameters: Test_H_FetchPackagePrices

            string role = "Test Package Pricer";
            string mainGroup = "00 - General";
            string subGroup = "00 - Vehicle, complete";
            string vehicleType = "All Scania Vehicles";
            string variantDescription = "VariantDescription";
            string reasonForPackage = "ReasonForPackage";
            string packageName = "PackageToManage";
            string packageDescription = "PackageDescription";
            string workorderText = "WorkorderText";
            string invoiceText = "InvoiceText";
            string parts = "1110055";
            string labour = "01015111";
            string sundry = "18";
            string approval = "GLOBAL\\testfprpricer (FPRPRICER IWNP)";
            string packageStatus = "SENT FOR REVIEW";

            // ---------------------------------------------------------

            LoginPage.LoginAs(role);
            verifyPageTitle(HomePage.mainPageTitle, driver);

            // Navigate Packages -> Create
            HomePage.menuPackages.Click();

            // Wait for page to load
            WaitForAjax(driver, 10, true);

            // Click on menu "Create"
            HomePage.menuCreate.Click();

            //Click on button Next
            WaitForElement(SearchPage.buttonNext);
            SearchPage.buttonNext.Click();

            // Fill out description form
            WaitForElement(DescriptionPage.listMainGroup);
            DescriptionPage.ToFillOutDescription(mainGroup, subGroup, vehicleType, variantDescription, reasonForPackage, packageName, packageDescription, workorderText, invoiceText);

            // Click button Next
            DescriptionPage.buttonNext.Click();

            // Fill out content form
            WaitForElements(ContentPage.allDropDownLists);
            ContentPage.ChoosePartLabourAndSundry(parts, labour, sundry);

            WaitForElement(ContentPage.checkbox_MAIN_PART);
            ContentPage.checkbox_MAIN_PART.Click();

            // Click button Next
            DescriptionPage.buttonNext.Click();

            Thread.Sleep(3000);

            PreviewPage.buttonApprove.Click();

            Thread.Sleep(3000);

            PreviewPage.buttonApproveOnConfirmationDialog.Click();

            Thread.Sleep(3000);

            PreviewPage.buttonNext.Click();

            Thread.Sleep(3000);

            // Find and click on the market
            PricingPage.ToFindMarket("Sweden");

            Thread.Sleep(3000);

            PricingPage.buttonPriceForGroup();

            Thread.Sleep(3000);

            WaitForElements(PricingPage.listWithDiscountButtons);

            PricingPage.buttonOpenListWithParts.Click();

            Thread.Sleep(3000);

            // Set discount
            PricingPage.ToSetDiscountPercentage(5.ToString());

            WaitForAjax(driver, 2, true);

            // Fetch all fields
            double QTY = Convert.ToDouble(PricingPage.fieldQuantity.Text);
            double CostPrice = PricingPage.ToDouble(PricingPage.fieldCost);
            double RetailPrice = PricingPage.ToDouble(PricingPage.fieldRetailPrice);
            //  + discountPercentage
            double SalesNetPrice = PricingPage.ToDouble(PricingPage.fieldSalesNetPrice);
            double TotalCost = PricingPage.ToDouble(PricingPage.fieldTotalCost);
            double TotalGross = PricingPage.ToDouble(PricingPage.fieldTotalGross);
            double TotalNet = PricingPage.ToDouble(PricingPage.fieldTotalNet);
            double GrossProfitAmount = PricingPage.ToDouble(PricingPage.fieldGrossProfitAmount);
            double GrossProfitPercentage = PricingPage.ToDouble(PricingPage.fieldGrossProfitPercentage);


            // Write to excel file
            WriteToExcel = new WriteToExcel();

            double[] data = new double[10] { QTY,
                                            CostPrice, 
                                            RetailPrice,  
                                            5, 
                                            SalesNetPrice, 
                                            TotalCost,
                                            TotalGross,
                                            TotalNet,
                                            GrossProfitAmount,
                                            GrossProfitPercentage};

            WriteToExcel.WriteToRange("CalculationData", data, "A2", "J2");

        }


    }
}
