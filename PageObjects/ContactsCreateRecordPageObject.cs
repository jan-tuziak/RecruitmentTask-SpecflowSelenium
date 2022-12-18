using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTaskSpecflowSelenium.PageObjects
{
    internal class ContactsCreateRecordPageObject
    {
        //The URL of the Create Contact page to be opened in the browser
        private const string LoginUrl = "https://demo.1crmcloud.com/index.php?module=Contacts&action=EditView&record=&return_module=Contacts&return_action=DetailView";

        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 10;

        WebDriverWait webDriverWait;

        public ContactsCreateRecordPageObject(IWebDriver webDriver) 
        {
            _webDriver = webDriver;
            webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        }

        //Finding elements by ID
        private IWebElement FirstNameInput => _webDriver.FindElement(By.Id("DetailFormfirst_name-input"));
        private IWebElement LastNameInput => _webDriver.FindElement(By.Id("DetailFormlast_name-input"));
        private IWebElement BusinessRoleInput => _webDriver.FindElement(By.Id("DetailFormbusiness_role-input"));
        private IWebElement BusinessRoleAdmin => _webDriver.FindElement(By.CssSelector("div#DetailFormbusiness_role-input-popup>div:first-child>div:nth-child(6)"));
        private IWebElement CategoryInput => _webDriver.FindElement(By.Id("DetailFormcategories-input"));
        private IWebElement CategorySearchList => _webDriver.FindElement(By.CssSelector("div#DetailFormcategories-input-search-text>input.input-text"));
        //private IWebElement CategoryCustomers => _webDriver.FindElement(By.XPath("//div[@class='option-cell input-label ' and text()='Customers']"));
        //private IWebElement CategorySuppliers => _webDriver.FindElement(By.XPath("//div[@class='option-cell input-label ' and text()='Suppliers']"));
        private void EnterFirstName(string firstName)
        {
            //Clear text box
            FirstNameInput.Clear();
            //Enter text
            FirstNameInput.SendKeys(firstName);
        }

        private void EnterLastName(string lastName)
        {
            //Clear text box
            LastNameInput.Clear();
            //Enter text
            LastNameInput.SendKeys(lastName);
        }

        private void ChooseBusinessRole(string businessRole)
        {
            Thread.Sleep(1000);
            BusinessRoleInput.Click();
            Thread.Sleep(1000);
            BusinessRoleAdmin.Click();
        }

        internal void CreateNewContact(string firstName, string lastName, string role, string category1, string category2)
        {
            Thread.Sleep(300);
            EnterFirstName(firstName);
            Thread.Sleep(300);
            EnterLastName(lastName);
            Thread.Sleep(300);
            ChooseBusinessRole(role);
            Thread.Sleep(300);
            AddCategory(category1);
            Thread.Sleep(300);
            AddCategory(category2);
            Thread.Sleep(300);
        }

        private void AddCategory(string category)
        {
            CategoryInput.Click();
            Thread.Sleep(500);
            CategorySearchList.Click();
            CategorySearchList.SendKeys(category);
            Thread.Sleep(500);
            CategorySearchList.SendKeys(Keys.Enter);
        }
    }
}
