using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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

        WebDriverWait wait;

        public ContactsCreateRecordPageObject(IWebDriver webDriver) 
        {
            _webDriver = webDriver;
            wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        }

        //Finding elements by ID and Css
        private By FirstNameInput => By.Id("DetailFormfirst_name-input");
        private By LastNameInput => By.Id("DetailFormlast_name-input");
        private By BusinessRoleInput => By.Id("DetailFormbusiness_role-input");
        private By BusinessRoleAdmin => By.CssSelector("div#DetailFormbusiness_role-input-popup>div:first-child>div:nth-child(6)");
        private By CategoryInput => By.Id("DetailFormcategories-input");
        private By CategorySearchList => By.CssSelector("div#DetailFormcategories-input-search-text>input.input-text");
        private By SaveContactButton => By.Id("DetailForm_save-label");
        private By ContactDuplicateMessage => By.CssSelector("form#Save>table>tbody>tr>td");
        private By ContactDuplicateSaveButton=> By.Name("SubPanel_save");

        private void EnterFirstName(string firstName)
        {
            //Clear text box
            wait.Until(driver => CommonElements.Appears(driver, FirstNameInput));
            _webDriver.FindElement(FirstNameInput).Clear();
            //Enter text
            _webDriver.FindElement(FirstNameInput).SendKeys(firstName);
        }

        private void EnterLastName(string lastName)
        {
            //Clear text box
            wait.Until(driver => CommonElements.Appears(driver, LastNameInput));
            _webDriver.FindElement(LastNameInput).Clear();
            //Enter text
            _webDriver.FindElement(LastNameInput).SendKeys(lastName);
        }

        private void ChooseBusinessRole(string businessRole)
        {
            wait.Until(driver => CommonElements.Appears(driver, BusinessRoleInput));
            _webDriver.FindElement(BusinessRoleInput).Click();

            wait.Until(driver => CommonElements.Appears(driver, BusinessRoleAdmin));
            _webDriver.FindElement(BusinessRoleAdmin).Click();
        }

        internal void CreateNewContact(string firstName, string lastName, string role, string category1, string category2)
        {
            CommonElements ce = new CommonElements(_webDriver);
            ce.WaitForStatusToDisappear();
            EnterFirstName(firstName);
            EnterLastName(lastName);
            ChooseBusinessRole(role);
            AddCategory(category1);
            AddCategory(category2);
            _webDriver.FindElement(SaveContactButton).Click();
            wait.Until(driver => CommonElements.Appears(driver, ContactDuplicateMessage));
            try
            {
                if (_webDriver.FindElement(ContactDuplicateMessage).Text.Contains("This contact may be a duplicate of an existing contact. You may either click on Save to continue creating this new contact with the previously entered data or you may click Cancel."))
                    _webDriver.FindElement(ContactDuplicateSaveButton).Click();
            }
            catch { }

        }

        private void AddCategory(string category)
        {
            _webDriver.FindElement(CategoryInput).Click();
            _webDriver.FindElement(CategorySearchList).Click();
            _webDriver.FindElement(CategorySearchList).SendKeys(category);
            _webDriver.FindElement(CategorySearchList).SendKeys(Keys.Enter);
        }
    }
}
