using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTaskSpecflowSelenium.PageObjects
{
    internal class LoginPageObject
    {
        //The URL of the login page to be opened in the browser
        private const string LoginUrl = "https://demo.1crmcloud.com/";

        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 5;

        public LoginPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        //Finding elements by ID
        private IWebElement UserName => _webDriver.FindElement(By.Id("login_user"));
        private IWebElement Password => _webDriver.FindElement(By.Id("login_pass"));
        private IWebElement Login => _webDriver.FindElement(By.Id("login_button"));
        private SelectElement Theme => new SelectElement(_webDriver.FindElement(By.Id("login_theme")));

        public void LoginAsAdmin()
        {
            this.GoTo();
            this.EnterUserName("admin");
            this.EnterPassword("admin");
            _webDriver.Manage().Window.Maximize();
            this.SelectTheme();
            this.ClickLogin();
        }

        public void GoTo()
        {
            _webDriver.Navigate().GoToUrl(LoginUrl);

            //Login Page and Home Page share the same URL.
            //Make sure the user is logged out.
            EnsureTheUserIsLoggedOut();
        }

        public void EnterUserName(string userName)
        {
            //Clear text box
            UserName.Clear();
            //Enter text
            UserName.SendKeys(userName);
        }

        public void EnterPassword(string password)
        {
            //Clear text box
            Password.Clear();
            //Enter text
            Password.SendKeys(password);
        }

        public void SelectTheme()
        {
            this.Theme.SelectByValue("Flex");
        }

        public void ClickLogin()
        {
            //Click the add button
            Login.Click();
        }

        public void EnsureTheUserIsLoggedOut()
        {
            //Open the login page in the browser if not opened yet
            if (_webDriver.Url != LoginUrl)
            {
                _webDriver.Url = LoginUrl;
            }

            if(!this.Login.Enabled || !this.Login.Displayed) //Check if login field exists
            //If not logout
            {
                HomePageObject homePageObject = new HomePageObject(_webDriver);
                homePageObject.Logout();
            }
        }

        //public string WaitForEmptyResult()
        //{
        //    //Wait for the result to be empty
        //    return WaitUntil(
        //        () => ResultElement.GetAttribute("value"),
        //        result => result == string.Empty);
        //}

        ///// <summary>
        ///// Helper method to wait until the expected result is available on the UI
        ///// </summary>
        ///// <typeparam name="T">The type of result to retrieve</typeparam>
        ///// <param name="getResult">The function to poll the result from the UI</param>
        ///// <param name="isResultAccepted">The function to decide if the polled result is accepted</param>
        ///// <returns>An accepted result returned from the UI. If the UI does not return an accepted result within the timeout an exception is thrown.</returns>
        //private T WaitUntil<T>(Func<T> getResult, Func<T, bool> isResultAccepted) where T : class
        //{
        //    var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        //    return wait.Until(driver =>
        //    {
        //        var result = getResult();
        //        if (!isResultAccepted(result))
        //            return default;

        //        return result;
        //    });

        //}
    }
}
