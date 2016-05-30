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
    public class TestCase_WorkingWithPackages : BaseSetUp
    {

        /// <summary>
        /// Create a new package with the status "New package"
        /// </summary>
        [TestCase("Test Package Creator")]
        [TestCase("Test Package Pricer")]
        [TestCase("Test Limited Viewer")]
        [TestCase("Test Extended Viewer")]
        [TestCase("Test Controller")]
        [TestCase("Test System Admin")]
        public void Test_A_0_CreateNewPackage(string role)
        {

            // TESTparameters: Test_A_CreateNewPackage

            string mainGroup = "00 - General";
            string subGroup = "00 - Vehicle, complete";
            string vehicleType = "All Scania Vehicles";
            string variantDescription = "VariantDescription";
            string reasonForPackage = "ReasonForPackage";
            string packageName = "PackageToManage";
            string packageDescription = "PackageDescription";
            string workorderText = "WorkorderText";
            string invoiceText = "InvoiceText";

            // ---------------------------------------------------------

            LoginPage.LoginAs(role);
            verifyPageTitle(HomePage.mainPageTitle, driver);
            
            IWebElement BigIcon_Create = searchElementByText("Create", HomePage.allIcons);
            BigIcon_Create.Click();
            
            WaitForElement(SearchPage.buttonNext);
            SearchPage.buttonNext.Click();

            // Fill out description form
            WaitForElement(DescriptionPage.listMainGroup);
            DescriptionPage.ToFillOutDescription(mainGroup, subGroup, vehicleType, variantDescription, reasonForPackage, packageName, packageDescription, workorderText, invoiceText);

            // Save the package
            DescriptionPage.buttonSave.Click();

            // Verify that the package was saved successfully
            WaitForElement(DescriptionPage.alertMessage);  
            StringAssert.Contains("Success! Package has been saved", DescriptionPage.alertMessage.Text, "Error when create a package with the role -" + role + " -");
        }

        /// <summary>
        /// Create a new package with the status "Draft package" 
        /// </summary>
        [TestCase("Test Package Creator")]
        [TestCase("Test Package Pricer")]
        [TestCase("Test Limited Viewer")]
        [TestCase("Test Extended Viewer")]
        [TestCase("Test Controller")]
        [TestCase("Test System Admin")]
        public void Test_A_1_CreateDraftPackage(string role)
        {

            // TESTparameters: Test_B_CreateDraftPackage

            string mainGroup = "00 - General";
            string subGroup = "00 - Vehicle, complete";
            string vehicleType = "All Scania Vehicles";
            string variantDescription = "VariantDescription";
            string reasonForPackage = "ReasonForPackage";
            string packageName = "PackageToManage";
            string packageDescription = "PackageDescription";
            string workorderText = "WorkorderText";
            string invoiceText = "InvoiceText";
            string parts = "1301111";
            string labour = "01015111";
            string sundry = "18";

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

            packageId = ContentPage.GetPackageId();

            // Save the package
            ContentPage.buttonSave.Click();

            // Verify that the package was saved successfully
            WaitForElement(DescriptionPage.alertMessage);
            StringAssert.Contains("Success! Package has been saved", DescriptionPage.alertMessage.Text, "Error when create a package with the role -" + role + " -");
        }
        
        /// <summary>
        /// Create a package with the status "New package" then go to manage package and add content to a package, 
        /// verify that the package status is changed to "Draft package"
        /// </summary>
        [TestCase("Test Package Creator")]
        [TestCase("Test Package Pricer")]
        [TestCase("Test Limited Viewer")]
        [TestCase("Test Extended Viewer")]
        [TestCase("Test Controller")]
        [TestCase("Test System Admin")]
        public void Test_A_2_ManagePackageAddContent(string role)
        {
            // TESTparameters: Test_C_ManagePackageAddContent

            string mainGroup = "00 - General";
            string subGroup = "00 - Vehicle, complete";
            string vehicleType = "All Scania Vehicles";
            string variantDescription = "VariantDescription";
            string reasonForPackage = "ReasonForPackage";
            string packageName = "PackageToManage";
            string packageDescription = "PackageDescription";
            string workorderText = "WorkorderText";
            string invoiceText = "InvoiceText";
            string parts = "1301111";
            string labour = "01015111";
            string sundry = "18";
            string approval = "GLOBAL\\testfprpricer (FPRPRICER IWNP)";

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

            // Generate package name with the current timestamp 
            packageName = packageName + " " + DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");

            // Fill out description form
            WaitForElement(DescriptionPage.listMainGroup);
            DescriptionPage.ToFillOutDescription(mainGroup, subGroup, vehicleType, variantDescription, reasonForPackage, packageName, packageDescription, workorderText, invoiceText);

            // Save the package
            DescriptionPage.buttonSave.Click();

            // Close toast message
            ContentPage.alertMessage.Click();

            // Navigate Packages -> Manage 
            
            PackagesMenu.buttonManage.Click();
            Thread.Sleep(2000);

            // Search for the package

            ManagePackages.fieldSearch.SendKeys(packageName);
            Thread.Sleep(2000);
            ManagePackages.buttonViewPackage.Click();

            // Open the pakage in a new tab 
            string tabManagePackage = RememberMyCurrentWindow(driver);
            SwitchToWindow(tabManagePackage, DescriptionPage.createPackageTitle, driver);

            // Wait for button Edit on Preview page and do click on it
            WaitForElement(PreviewPage.buttonEdit);
            PreviewPage.buttonEdit.Click();
            
            // Navigate to the Content page
            DescriptionPage.buttonContent.Click();
            WaitForElements(ContentPage.allDropDownLists);

            // Fill out content form
            ContentPage.ChoosePartLabourAndSundry(parts, labour, sundry);

            WaitForElement(ContentPage.checkbox_MAIN_PART);
            ContentPage.checkbox_MAIN_PART.Click();

            packageId = ContentPage.GetPackageId();

            // Save the package
            ContentPage.buttonSave.Click();

            // Verify that the package was saved successfully
            WaitForElement(ContentPage.alertMessage);
            StringAssert.Contains("Success! Package has been saved", DescriptionPage.alertMessage.Text, "Error when create a package with the role -" + role + " -");
        }
        
        /// <summary>
        /// The role: Test Package Creator
        /// The flow: Create a package -> Add content -> Send for approval
        /// </summary>
        [TestCase("Test Package Creator")]
        [TestCase("Test Limited Viewer")]
        [TestCase("Test Extended Viewer")]
        [TestCase("Test Controller")]
        public void Test_A_3_SendForApproval(string role)
        {
            // TESTparameters: Test_D_SendForApproval

            string mainGroup = "00 - General";
            string subGroup = "00 - Vehicle, complete";
            string vehicleType = "All Scania Vehicles";
            string variantDescription = "VariantDescription";
            string reasonForPackage = "ReasonForPackage";
            string packageName = "PackageToManage";
            string packageDescription = "PackageDescription";
            string workorderText = "WorkorderText";
            string invoiceText = "InvoiceText";
            string parts = "1301111";
            string labour = "01015111";
            string sundry = "18";
            string approval = "testfprpricer";
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

            Thread.Sleep(2000);

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

            // SAVE PACKAGE ID
            packageId = ContentPage.GetPackageId();

            // Save the package
            DescriptionPage.buttonSave.Click();

            // Verify that the package was saved successfully
            WaitForElement(DescriptionPage.alertMessage);

            // Click button Next
            DescriptionPage.buttonNext.Click();
            WaitForElement(PreviewPage.dropdownSelectApprover);

            Thread.Sleep(3000);

            // Select for approval
            selectFromDropDownByText(approval, PreviewPage.dropdownSelectApprover);

            // Wait for button Send for approval will be active
            WaitForElement(PreviewPage.buttonSendForApproval);

            // Click button Send for approval
            PreviewPage.buttonSendForApproval.Click();

            // Wait for confirmation dialog with button Send for approval
            WaitForElement(PreviewPage.buttonSendForApprovalConfirm);

            Thread.Sleep(1000);

            // Click button send for approval on confirmation dialog
            PreviewPage.buttonSendForApprovalConfirm.Click();

            // Verify that the package status was changed
            Thread.Sleep(2000);
            ManagePackages.fieldSearch.SendKeys(packageId);
            
            Thread.Sleep(5000);
            ManagePackages.buttonViewPackage.Click();
            Thread.Sleep(2000);

            // Open the pakage in a new tab 
            string tabManagePackage = RememberMyCurrentWindow(driver);
            SwitchToWindow(tabManagePackage, DescriptionPage.createPackageTitle, driver);

            Thread.Sleep(3000);

            // Verify that the package status was changed
            WaitForElement(PreviewPage.packageStatus);
            StringAssert.Contains(packageStatus, PreviewPage.packageStatus.Text, "Error when create a package with the role -" + role + " -");
        }

        /// <summary>
        /// The role: Test Package Creator, Test Package Pricer
        /// The flow: (As Test Package Creator) Create a package -> Add content -> Send for approval 
        ///           (As Test Package Pricer) -> Approve the package                               
        /// </summary>
        [TestCase("Test Package Creator")]
        [TestCase("Test Limited Viewer")]
        [TestCase("Test Extended Viewer")]
        [TestCase("Test Controller")]
        public void Test_A_4_SendForApprovalAndApprove(string role)
        {

            // TESTparameters: Test_E_SendForApprovalAndApprove

            string role_TestPackgeCreator = "Test Package Creator";
            string role_TestPackagePricer = "Test Package Pricer";
            string mainGroup = "00 - General";
            string subGroup = "00 - Vehicle, complete";
            string vehicleType = "All Scania Vehicles";
            string variantDescription = "VariantDescription";
            string reasonForPackage = "ReasonForPackage";
            string packageName = "PackageToManage";
            string packageDescription = "PackageDescription";
            string workorderText = "WorkorderText";
            string invoiceText = "InvoiceText";
            string parts = "1301111";
            string labour = "01015111";
            string sundry = "18";
            string approval = "GLOBAL\testfprpricer (FPRPRICER IKB)";
            string packageStatus = "PENDING REVIEW";

            // ---------------------------------------------------------

            LoginPage.LoginAs(role_TestPackgeCreator);
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

            // SAVE PACKAGE ID
            packageId = ContentPage.GetPackageId();

            // Save the package
            DescriptionPage.buttonSave.Click();

            // Verify that the package was saved successfully
            WaitForElement(DescriptionPage.alertMessage);

            // Click button Next
            DescriptionPage.buttonNext.Click();
            WaitForElement(PreviewPage.dropdownSelectApprover);

            Thread.Sleep(3000);

            // Select for approval
            selectFromDropDownByText(approval, PreviewPage.dropdownSelectApprover);

            // Wait for button Send for approval will be active
            WaitForElement(PreviewPage.buttonSendForApproval);

            // Click button Send for approval
            PreviewPage.buttonSendForApproval.Click();

            // Wait for confirmation dialog with button Send for approval
            WaitForElement(PreviewPage.buttonSendForApprovalConfirm);

            Thread.Sleep(1000);

            // Click button send for approval on confirmation dialog
            PreviewPage.buttonSendForApprovalConfirm.Click();

            Thread.Sleep(3000);

            HomePage.buttonLoggedUser.Click();
            HomePage.buttonSwitchUser.Click();
            
            Thread.Sleep(2000);
            
            LoginPage.LoginAs(role_TestPackagePricer);
            verifyPageTitle(HomePage.mainPageTitle, driver);

            IWebElement BigIcon_Approve = searchElementByText("Approve", HomePage.allIcons);
            BigIcon_Approve.Click();

            ApprovePage.fieldSearch.SendKeys(packageId);

            Thread.Sleep(2000);

            ApprovePage.buttonViewPackage.Click();

            // Open the pakage in a new tab 
            string tabManagePackage = RememberMyCurrentWindow(driver);
            string title = "CreatePackage - Fixed Price Repair";
            SwitchToWindow(tabManagePackage, title, driver);

            Thread.Sleep(3000);

            // Verify that the package status was changed
            WaitForElement(ApprovePage.packageStatus);
            StringAssert.Contains(packageStatus, ApprovePage.packageStatus.Text, "Error!");

        }

        /// <summary>
        /// Manage packages -> Open a package -> Verify that the package was opened in a new tab
        /// </summary>
        [Test]
        public void Test_A_5_ManageOpenPackageNewTab()
        {
            LoginPage.LoginAsRealUser();
            verifyPageTitle(HomePage.mainPageTitle, driver);

            Thread.Sleep(1000);

            // Navigate Packages -> Create
            HomePage.menuPackages.Click();
            Thread.Sleep(1000);
            //HomePage.menuPackages.Click();

            // Wait for page to load
            WaitForAjax(driver, 10, true);

            Thread.Sleep(2000);

            // Click on the first package in the list
            clickOnElementIfItIsClickable(ManagePackages.buttonsPackageView);

            // Wait for 1 sec
            Thread.Sleep(1000);

            // Read collection with all tabs
            ReadOnlyCollection<string> handles = driver.WindowHandles;

            // Verify that the package was opened in a new tab
            Assert.AreEqual(2, handles.Count, "Package was opened in the same window!");
        }

        /// <summary>
        /// Packages -> Search -> Advance Search -> Verify that the package was opened in a new tab
        /// </summary>
        [Test]
        public void Test_A_6_AdvanceSearchOpenPackageNewTab()
        {
            LoginPage.LoginAsRealUser();
            verifyPageTitle(HomePage.mainPageTitle, driver);

            // Navigate Packages -> Create
            HomePage.menuPackages.Click();
            Thread.Sleep(1000);
            //HomePage.menuPackages.Click();

            // Wait for page to load
            WaitForAjax(driver, 10, true);

            // Click on button Search on menu Packages
            PackagesMenu.buttonSearch.Click();

            Thread.Sleep(2000);

            // Click on button Advance Search on Search page
            //SearchPage.buttonAdvanceSearch.Click();

            // Click on buttton Search on Search page
            SearchPage.buttonSearch.Click();

            Thread.Sleep(2000);

            // Click on the first package in the list

            clickOnElementIfItIsClickable(SearchPage.buttonsPackageView);

            // Wait 1 sec
            Thread.Sleep(1000);

            // Read collection with all tabs
            ReadOnlyCollection<string> handles = driver.WindowHandles;

            // Verify that the package was opened in a new tab
            Assert.AreEqual(2, handles.Count, "Package was opened in the same window!");
        }

        [Test]
        public void Test_A_7_ImportPackage()
        {

            // TESTparameters: Test_A_CreateNewPackage

            string role = "Test Package Creator";
            string mainGroup = "01 - Engine";
            string subGroup = "05 - Cylinder block";
            string packageCode = "9004";

            // ---------------------------------------------------------

            LoginPage.LoginAs(role);
            verifyPageTitle(HomePage.mainPageTitle, driver);

            HomePage.ClickButtonImport();

            Thread.Sleep(2000);

            selectFromDropDownByText(mainGroup, ImportPage.dropDownMainGroup);

            selectFromDropDownByText(subGroup, ImportPage.dropDownSubGroup);

            ImportPage.inputFieldPackageCode.SendKeys(packageCode);

            ImportPage.buttonSearch.Click();

            Thread.Sleep(2000);

            

            /*

            WaitForElement(SearchPage.buttonNext);
            SearchPage.buttonNext.Click();

            // Fill out description form
            WaitForElement(DescriptionPage.listMainGroup);
            DescriptionPage.ToFillOutDescription(mainGroup, subGroup, vehicleType, variantDescription, reasonForPackage, packageName, packageDescription, workorderText, invoiceText);

            // Save the package
            DescriptionPage.buttonSave.Click();

            // Verify that the package was saved successfully
            WaitForElement(DescriptionPage.alertMessage);
            StringAssert.Contains("Success! Package has been saved", DescriptionPage.alertMessage.Text, "Error when create a package with the role -" + role + " -");
             * */
        }

        }
}
