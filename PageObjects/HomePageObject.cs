using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTaskSpecflowSelenium.PageObjects
{
    internal class HomePageObject
    {
        //The URL of the login page to be opened in the browser
        private const string LoginUrl = "https://demo.1crmcloud.com/";

        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 5;

        public HomePageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        //Finding elements by ID
        private IWebElement HamburgerMenu => _webDriver.FindElement(By.CssSelector("label.meta-options-dropdown-button"));
        private IWebElement LogoutLink => _webDriver.FindElement(By.CssSelector("div.meta-options-dropdown>a:first-child"));
        private IWebElement TabSalesAndMarketing => _webDriver.FindElement(By.Id("grouptab-1"));
        private IWebElement LinkContacts => _webDriver.FindElement(By.CssSelector("div.tab-nav-sub-wrap>div:nth-child(4)>div:nth-child(3)>a.menu-tab-sub-list"));


        public void Logout()
        {
            this.GoTo();
            this.HamburgerMenu.Click();
            this.LogoutLink.Click();
        }

        public void ClickLogout()
        {
            HamburgerMenu.Click();
            LogoutLink.Click();
        }

        public void GoTo()
        {
            _webDriver.Navigate().GoToUrl(LoginUrl);

            //Home Page and Login Page share the same URL.
            //Make sure the user is logged in.
            EnsureTheUserIsLoggedIn();
        }

        public void EnsureTheUserIsLoggedIn()
        {
            //Open the home page in the browser if not opened yet
            if (_webDriver.Url != LoginUrl)
            {
                _webDriver.Url = LoginUrl;
            }
            
            if (!this.HamburgerMenu.Displayed || !this.HamburgerMenu.Enabled) //Check if hamburger menu exists
            //If not login as admin
            {
                LoginPageObject loginPageObject = new LoginPageObject(_webDriver);
                loginPageObject.LoginAsAdmin();
            }
        }

        internal void NavigateTo(string tabName)
        {
            IWebElement tab;
            IWebElement link;

            switch (tabName)
            {
                case "Contacts under Sales & Marketing":
                    tab = TabSalesAndMarketing;
                    link = LinkContacts;
                    break;
                default: throw new ArgumentException("Invalid Tab and Link name");
            }

            //Hover over the Tab
            Actions action = new Actions(_webDriver);
            action.MoveToElement(tab).Perform();

            //Click the link
            link.Click();
        }
    }
}
