using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTaskSpecflowSelenium.PageObjects
{
    internal class HomePageObject
    {
        //The URL of the home page to be opened in the browser
        private const string LoginUrl = "https://demo.1crmcloud.com/";

        //The Selenium web driver to automate the browser
        private readonly IWebDriver driver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 5;

        WebDriverWait wait;

        public HomePageObject(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        }

        //Finding elements by ID
        public By HamburgerMenu => By.CssSelector("label.meta-options-dropdown-button");
        private By LogoutLink => By.CssSelector("div.meta-options-dropdown>a:first-child");
        private By TabSalesAndMarketing => By.Id("grouptab-1");
        private By LinkContacts => By.CssSelector("div.tab-nav-sub-wrap>div:nth-child(4)>div:nth-child(3)>a.menu-tab-sub-list");
        private By TabReportsAndSettings => By.Id("grouptab-5");
        private By LinkReports => By.CssSelector("div.tab-nav-sub-wrap>div:nth-child(12)>div:first-child>a.menu-tab-sub-list");
        private By LinkActivityLog => By.CssSelector("div.tab-nav-sub-wrap>div:nth-child(12)>div:nth-child(3)>a.menu-tab-sub-list");

        public void Logout()
        {
            this.GoTo();
            driver.FindElement(HamburgerMenu).Click();
            driver.FindElement(LogoutLink).Click();
        }

        public void ClickLogout()
        {
            driver.FindElement(HamburgerMenu).Click();
            driver.FindElement(LogoutLink).Click();
        }

        public void GoTo()
        {
            driver.Navigate().GoToUrl(LoginUrl);

            //Home Page and Login Page share the same URL.
            //Make sure the user is logged in.
            EnsureTheUserIsLoggedIn();
        }

        public void EnsureTheUserIsLoggedIn()
        {
            //Open the home page in the browser if not opened yet
            if (driver.Url != LoginUrl)
            {
                driver.Url = LoginUrl;
            }
            
            if (!driver.FindElement(HamburgerMenu).Displayed || !driver.FindElement(HamburgerMenu).Enabled) //Check if hamburger menu exists
            //If not login as admin
            {
                LoginPageObject loginPageObject = new LoginPageObject(driver);
                loginPageObject.LoginAsAdmin();
            }
        }

        internal void NavigateTo(string tabName)
        {
            By tab;
            By link;
            CommonElements ce = new CommonElements(driver);
            ce.WaitForStatusToDisappear();

            switch (tabName)
            {
                case "Contacts under Sales & Marketing":
                    tab = TabSalesAndMarketing;
                    link = LinkContacts;
                    break;
                case "Reports under Reports & Settings":
                    tab = TabReportsAndSettings;
                    link = LinkReports;
                    break;
                case "Activity Log under Reports & Settings":
                    tab = TabReportsAndSettings;
                    link = LinkActivityLog;
                    break;
                default: throw new ArgumentException("Invalid Tab and/or Link name");
            }

            //Hover over the Tab
            wait.Until(driver => CommonElements.Appears(driver, tab));
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(tab)).Perform();

            //Click the link
            wait.Until(driver => CommonElements.Appears(driver, link));
            driver.FindElement(link).Click();
        }
    }
}
