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
using System.Collections;
using Scania.Selenium.Support.SelActions;
using Scania.Selenium.Support.Driver;

namespace SeleniumNunitTemplate
{

    [TestFixture]
    public class TestSuite_B_Calculation : SelActions
    {
        public LoginPage LoginPage;
        public HomePage HomePage;
        public DescriptionPage DescriptionPage;
        public ManagePackages ManagePackages;
        public ContentPage ContentPage;
        public PreviewPage PreviewPage;
        public SearchPage SearchPage;
        public PackagesMenu PackagesMenu;
        public PricingPage PricingPage;
        public CleanData CleanData;
        public ApprovePage ApprovePage;
        
        public string URL = Properties.Settings.Default.testURL;
        public string pathToFileWithTestData = @"C:\Users\MIVHDE\Downloads\myfile.xls";

        public double qty;
        public double cost;
        public double distributorCost;
        public double retailPrice;
        public double discount_Amount;
        public double discount_Percentage;
        public double salesNetPrice;
        public double totalNet;
        public double totalGross; 
        public double totalCost;
        public double grossProfit_Amount;
        public double grossProfit_Percentage; 
        public double totalCostDistributor;
        public double grossProfitDistributor_Amount;
        public double grossProfitDistributor_Percentage;

        public double ToDouble(string field)
        {

            string stringField = field.Replace("Kr", "").Replace("%", "").Replace(".", ",");
            double convertedField = Convert.ToDouble(stringField);
            return convertedField;
        }

        [TestFixtureSetUp]
        public void CreateFileWithTestData() 
        {
            
            Driver getDriver = new Driver();
            driver = getDriver.driverChrome();

            LoginPage = new LoginPage(driver);
            HomePage = new HomePage(driver);
            DescriptionPage = new DescriptionPage(driver);
            ManagePackages = new ManagePackages(driver);
            ContentPage = new ContentPage(driver);
            PreviewPage = new PreviewPage(driver);
            SearchPage = new SearchPage(driver);
            PackagesMenu = new PackagesMenu(driver);
            PricingPage = new PricingPage(driver);
            CleanData = new CleanData();
            ApprovePage = new ApprovePage(driver);

            driver.Navigate().GoToUrl(URL);

            // CreatePackage_AddTwoParts_Approve_SetDiscount_ExportToExcel

            string role = "Test Package Pricer";
            string mainGroup = "00 - General";
            string subGroup = "00 - Vehicle, complete";
            string vehicleType = "All Scania Vehicles";
            string variantDescription = "VariantDescription";
            string reasonForPackage = "ReasonForPackage";
            string packageName = "PackagePriceTwoParts";
            string packageDescription = "PackageDescription";
            string workorderText = "WorkorderText";
            string invoiceText = "InvoiceText";
            string parts = "1110055";
            string quantity = "2";

            // ---------------------------------------------------------

            LoginPage.LoginAsRealUser();
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

            // Choose a part
            WaitForElements(ContentPage.allDropDownLists);
            ContentPage.dropDownSelectPart(parts);
            ContentPage.buttonAddParts.Click();

            // Set quantity
            ContentPage.fieldQty.Clear();
            ContentPage.fieldQty.SendKeys(quantity);

            // Select main part
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

            Thread.Sleep(5000);

            WaitForAjax(driver, 2, true);

            driver.FindElement(By.CssSelector("[value=\"Export to Excel\"]")).Click();
            
            driver.Close();
            driver.Quit(); 
             
            

            ReadFromExcel getDataSource = new ReadFromExcel();
            List<string> dataArray = getDataSource.GetWorkSheet(pathToFileWithTestData, 1, "A3", "S3");

            qty = ToDouble(dataArray[4]);
            cost = ToDouble(dataArray[5]);
            distributorCost = ToDouble(dataArray[6]);
            retailPrice = ToDouble(dataArray[7]);
            discount_Amount = ToDouble(dataArray[8]);
            discount_Percentage = ToDouble(dataArray[9]);
            salesNetPrice = ToDouble(dataArray[10]);
            totalNet = ToDouble(dataArray[11]);
            totalGross = ToDouble(dataArray[12]);
            totalCost = ToDouble(dataArray[13]);
            grossProfit_Amount = ToDouble(dataArray[14]);
            grossProfit_Percentage = ToDouble(dataArray[15]);
            totalCostDistributor = ToDouble(dataArray[16]);
            grossProfitDistributor_Amount = ToDouble(dataArray[17]);
            grossProfitDistributor_Percentage = ToDouble(dataArray[18]);

        }

        [TestFixtureTearDown]
        public void DeleteFileWithTestData()
        {
            // Delete a file by using File class static method...
            if (System.IO.File.Exists(pathToFileWithTestData))
            {
                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    System.IO.File.Delete(pathToFileWithTestData);
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

        }

        /// <summary>
        /// Calculation SALES NET PRICE
        /// </summary>
        [Test]
        public void Test_B_1_Calculation_SalesNetPrice()

        {

            double salesNetPrice_Actual = salesNetPrice;
            double salesNetPrice_Calculated = Math.Round((retailPrice - (retailPrice / 100 * 5)), 2, MidpointRounding.AwayFromZero);

            // Verify that the Calculated "SALES NET PRICE" equals the Actual "SALES NET PRICE"
            Assert.AreEqual(salesNetPrice_Calculated, salesNetPrice, "SALES NET PRICE was calculated wrong");
            
        }

        /// <summary>
        /// Calculation TOTAL NET
        /// </summary>
        [Test]
        public void Test_B_2_Calculation_TotalNet()
        {

            double salesNetPrice_Calculated = (retailPrice - (retailPrice / 100 * 5));

            double totalNet_Actual = totalNet;
            double totalNet_Calculated = Math.Round((qty * salesNetPrice_Calculated), 2, MidpointRounding.AwayFromZero);

            // Verify that the Calculated "TOTAL NET" equals the Actual "TOTAL NET"
            Assert.AreEqual(totalNet_Calculated, totalNet_Actual, "TOTAL NET was calculated wrong");

        }

        /// <summary>
        /// Calculation TOTAL GROSS
        /// </summary>
        [Test]
        public void Test_B_3_Calculation_TotalGross()
        {

            
            double totalGross_Actual = totalGross;
            double totalGross_Calculated = Math.Round((qty * retailPrice), 2, MidpointRounding.AwayFromZero);

            // Verify that the Calculated "TOTAL GROSS" equals the Actual "TOTAL GROSS"
            Assert.AreEqual(totalGross_Calculated, totalGross_Actual, "TOTAL GROSS was calculated wrong");

        }

        /// <summary>
        /// Calculation TOTAL COST
        /// </summary>
        [Test]
        public void Test_B_4_Calculation_TotalCost()
        {


            double totalCost_Actual = totalCost;
            double totalCost_Calculated = Math.Round((qty * cost), 2, MidpointRounding.AwayFromZero);

            // Verify that the Calculated "TOTAL COST" equals the Actual "TOTAL COST"
            Assert.AreEqual(totalCost_Calculated, totalCost_Actual, "TOTAL COST was calculated wrong");

        }

        /// <summary>
        /// Calculation GROSS PROFIT AMOUNT
        /// </summary>
        [Test]
        public void Test_B_5_Calculation_Gross_Profit_Amount()
        {

            double totalCost_Calculated = qty * cost;
            double salesNetPrice_Calculated = retailPrice - (retailPrice / 100 * 5);
            double totalNet_Calculated = qty * salesNetPrice_Calculated;

            double grossProfit_Amount_Actual = grossProfit_Amount;
            double grossProfit_Amount_Calculated = Math.Round((totalNet_Calculated - totalCost_Calculated), 2, MidpointRounding.AwayFromZero);

            // Verify that the Calculated "GROSS PROFIT AMOUNT" equals the Actual "GROSS PROFIT AMOUNT"
            Assert.AreEqual(grossProfit_Amount_Calculated, grossProfit_Amount_Actual, "GROSS PROFIT AMOUNT was calculated wrong");

        }

        /// <summary>
        /// Calculation GROSS PROFIT PERCENTAGE
        /// </summary>
        [Test]
        public void Test_B_6_Calculation_Gross_Profit_Percentage()
        {

            double totalCost_Calculated = qty * cost;
            
            double salesNetPrice_Calculated = retailPrice - (retailPrice / 100 * 5);
            
            double totalNet_Calculated = qty * salesNetPrice_Calculated;

            double grossProfit_Amount_Calculated = totalNet_Calculated - totalCost_Calculated;

            double grossProfit_Percentage_Actual = grossProfit_Percentage;
            double grossProfit_Percentage_Calculated = Math.Round((grossProfit_Amount_Calculated / totalNet_Calculated * 100), 2, MidpointRounding.AwayFromZero);

            // Verify that the Calculated "GROSS PROFIT AMOUNT" equals the Actual "GROSS PROFIT AMOUNT"
            Assert.AreEqual(grossProfit_Percentage_Calculated, grossProfit_Percentage_Actual, "GROSS PROFIT PERCENTAGE was calculated wrong");

        }

        /// <summary>
        /// Calculation TOTAL COST DISTRIBUTOR 
        /// </summary>
        [Test]
        public void Test_B_7_Calculation_Total_Cost_Distributor()
        {

            double totalCostDitributor_Actual = totalCostDistributor;

            double totalCostDistributor_Calculated = Math.Round((qty * distributorCost), 2, MidpointRounding.AwayFromZero);

            // Verify that the Calculated "TOTAL COST DISTRIBUTOR" equals the Actual "TOTAL COST DISTRIBUTOR"
            Assert.AreEqual(totalCostDistributor_Calculated, totalCostDitributor_Actual, "TOTAL COST DISTRIBUTOR was calculated wrong");

        }

        /// <summary>
        /// Calculation GROSS PROFIT DISTRIBUTOR AMOUNT
        /// </summary>
        [Test]
        public void Test_B_8_Calculation_Gross_Profit_Distributor_Amount()
        {
            double grossProfitDistributor_Amount_Actual = grossProfitDistributor_Amount;

            double totalCost_Calculated = qty * cost;

            double totalCostDistributor_Calculated = Math.Round(qty * distributorCost);

            double grossProfitDistributor_Amount_Calculated = Math.Round((totalCost_Calculated - totalCostDistributor_Calculated), 2, MidpointRounding.AwayFromZero);

            // Verify that the Calculated "GROSS PROFIT DISTRIBUTOR AMOUNT" equals the Actual "GROSS PROFIT DISTRIBUTOR AMOUNT"
            Assert.AreEqual(grossProfitDistributor_Amount_Calculated, grossProfitDistributor_Amount_Actual, "GROSS PROFIT DISTRIBUTOR AMOUNT was calculated wrong");

        }

        /// <summary>
        /// Calculation GROSS PROFIT DISTRIBUTOR PERCENTAGE
        /// </summary>
        [Test]
        public void Test_B_9_Calculation_Gross_Profit_Distributor_Percentage()
        {
            double grossProfitDistributor_Percentage_Actual = grossProfitDistributor_Percentage;

            double totalCost_Calculated = qty * cost;
            Console.WriteLine(totalCost_Calculated);

            double totalCostDistributor_Calculated = Math.Round(qty * distributorCost);
            double grossProfitDistributor_Amount_Calculated = totalCost_Calculated - totalCostDistributor_Calculated;
            Console.WriteLine(grossProfitDistributor_Amount_Calculated);

            double grossProfitDistributor_Percentage_Calculated = Math.Round((grossProfitDistributor_Amount_Calculated/totalCost_Calculated * 100), 2, MidpointRounding.AwayFromZero);

            // Verify that the Calculated "GROSS PROFIT DISTRIBUTOR PERCENTAGE" equals the Actual "GROSS PROFIT DISTRIBUTOR PERCENTAGE"
            Assert.AreEqual(grossProfitDistributor_Percentage_Calculated, grossProfitDistributor_Percentage_Actual, "GROSS PROFIT DISTRIBUTOR PERCENTAGE was calculated wrong");

        }




        

    }


}
