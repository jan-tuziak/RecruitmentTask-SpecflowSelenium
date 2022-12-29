using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace RecruitmentTaskSpecflowSelenium.PageObjects
{
    internal class LoginPageObject
    {
        //The URL of the login page to be opened in the browser
        private const string LoginUrl = "https://demo.1crmcloud.com/";

        //The Selenium web driver to automate the browser
        private readonly IWebDriver driver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 10;

        public LoginPageObject(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        //Finding elements by ID
        private By UserName => By.Id("login_user");
        private By Password => By.Id("login_pass");
        private By Login => By.Id("login_button");
        private SelectElement Theme => new SelectElement(driver.FindElement(By.Id("login_theme")));

        public void LoginAsAdmin()
        {
            this.GoTo();
            this.EnterUserName("admin");
            this.EnterPassword("admin");
            driver.Manage().Window.Maximize();
            this.SelectTheme();
            this.ClickLogin();
        }

        public void GoTo()
        {
            driver.Navigate().GoToUrl(LoginUrl);

            //Login Page and Home Page share the same URL.
            //Make sure the user is logged out.
            EnsureTheUserIsLoggedOut();
        }

        public void EnterUserName(string userName)
        {
            //Clear text box
            driver.FindElement(UserName).Clear();
            //Enter text
            driver.FindElement(UserName).SendKeys(userName);
        }

        public void EnterPassword(string password)
        {
            //Clear text box
            driver.FindElement(Password).Clear();
            //Enter text
            driver.FindElement(Password).SendKeys(password);
        }

        public void SelectTheme()
        {
            this.Theme.SelectByValue("Flex");
        }

        public void ClickLogin()
        {
            //Click the add button
            driver.FindElement(Login).Click();
        }

        public void EnsureTheUserIsLoggedOut()
        {
            //Open the login page in the browser if not opened yet
            if (driver.Url != LoginUrl)
            {
                driver.Url = LoginUrl;
            }

            try  
            //Check if login field exists
            {
                var temp = driver.FindElement(Login).Enabled || driver.FindElement(Login).Displayed;
            }
            catch(Exception ex)
            //If not logout
            {
                HomePageObject homePageObject = new HomePageObject(driver);
                homePageObject.Logout();
            }
        }
    }
}
