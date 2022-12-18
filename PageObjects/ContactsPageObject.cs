using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RecruitmentTaskSpecflowSelenium.PageObjects
{
    internal class ContactsPageObject
    {
        //The URL of the home page to be opened in the browser
        private const string LoginUrl = "https://demo.1crmcloud.com/index.php?module=Contacts&action=index";

        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 5;

        public ContactsPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        //Finding elements by ID
        private IWebElement CreateContactButton => _webDriver.FindElement(By.CssSelector("#left-sidebar>div:nth-child(2)"));

        public void CreateContact()
        {
            CreateContactButton.Click();
        }
    }
}
