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

        //Finding elements by ID and Css
        private IWebElement FirstNameInput => _webDriver.FindElement(By.Id("DetailFormfirst_name-input"));
        private IWebElement LastNameInput => _webDriver.FindElement(By.Id("DetailFormlast_name-input"));
        private IWebElement BusinessRoleInput => _webDriver.FindElement(By.Id("DetailFormbusiness_role-input"));
        private IWebElement BusinessRoleAdmin => _webDriver.FindElement(By.CssSelector("div#DetailFormbusiness_role-input-popup>div:first-child>div:nth-child(6)"));
        private IWebElement CategoryInput => _webDriver.FindElement(By.Id("DetailFormcategories-input"));
        private IWebElement CategorySearchList => _webDriver.FindElement(By.CssSelector("div#DetailFormcategories-input-search-text>input.input-text"));
        private IWebElement SaveContactButton => _webDriver.FindElement(By.Id("DetailForm_save-label"));
        private IWebElement ContactDuplicateMessage => _webDriver.FindElement(By.CssSelector("form#Save>table>tbody>tr>td"));
        private IWebElement ContactDuplicateSaveButton=> _webDriver.FindElement(By.Name("SubPanel_save"));

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
            //TODO: Find a way to remove these Sleeps. Without them Selenium does not choose the Busniess Role
            Thread.Sleep(1000);
            BusinessRoleInput.Click();
            Thread.Sleep(1000);
            BusinessRoleAdmin.Click();
        }

        internal void CreateNewContact(string firstName, string lastName, string role, string category1, string category2)
        {
            EnterFirstName(firstName);
            EnterLastName(lastName);
            ChooseBusinessRole(role);
            AddCategory(category1);
            AddCategory(category2);
            SaveContactButton.Click();
            Thread.Sleep(2000);
            try
            {
                if (ContactDuplicateMessage.Text.Contains("This contact may be a duplicate of an existing contact. You may either click on Save to continue creating this new contact with the previously entered data or you may click Cancel."))
                    ContactDuplicateSaveButton.Click();
            }
            catch { }

        }

        private void AddCategory(string category)
        {
            CategoryInput.Click();
            CategorySearchList.Click();
            CategorySearchList.SendKeys(category);
            CategorySearchList.SendKeys(Keys.Enter);
        }
    }
}
