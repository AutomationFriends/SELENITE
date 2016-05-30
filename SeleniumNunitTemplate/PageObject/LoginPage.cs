using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Threading;
using Scania.Selenium.Support.SelActions;
using Scania.Selenium.Support.Logging;

namespace SeleniumNunitTemplate
{

public class LoginPage:SelActions
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            PageFactory.InitElements(driver, this);
        }

        
        [FindsBy(How = How.Id, Using = "testaccount")]
        public IWebElement dropdownAccountType;

        [FindsBy(How = How.ClassName, Using = "icon-unlock")]
        public IWebElement buttonSignIn;
        

        /// <summary>
        /// Log in with different roles
        /// </summary>
        /// <param name="role">Test Package Creator, Test Package Pricer, </param>
        public void LoginAs(string role)
        {
            Logging logging = new Logging();
            //logging.SimpleLogging();
            selectFromDropDownByText(role, dropdownAccountType);
            buttonSignIn.Click();

        }        
        
        public void LoginAsRealUser()
        {
            selectFromDropDownByText("Other", dropdownAccountType);
            driver.FindElement(By.Id("username")).SendKeys("mivhde");
            driver.FindElement(By.Id("password")).SendKeys("Scania2016!");
            buttonSignIn.Click();
        }

        
    }


}
