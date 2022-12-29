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
        private readonly IWebDriver driver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 10;

        WebDriverWait wait;

        public ContactsCreateRecordPageObject(IWebDriver webDriver) 
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
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
            driver.FindElement(FirstNameInput).Clear();
            //Enter text
            driver.FindElement(FirstNameInput).SendKeys(firstName);
        }

        private void EnterLastName(string lastName)
        {
            //Clear text box
            wait.Until(driver => CommonElements.Appears(driver, LastNameInput));
            driver.FindElement(LastNameInput).Clear();
            //Enter text
            driver.FindElement(LastNameInput).SendKeys(lastName);
        }

        private void ChooseBusinessRole(string businessRole)
        {
            wait.Until(driver => CommonElements.Appears(driver, BusinessRoleInput));
            driver.FindElement(BusinessRoleInput).Click();

            wait.Until(driver => CommonElements.Appears(driver, BusinessRoleAdmin));
            driver.FindElement(BusinessRoleAdmin).Click();
        }

        internal void CreateNewContact(string firstName, string lastName, string role, string category1, string category2)
        {
            CommonElements ce = new CommonElements(driver);
            ce.WaitForStatusToDisappear();
            EnterFirstName(firstName);
            EnterLastName(lastName);
            ChooseBusinessRole(role);
            AddCategory(category1);
            AddCategory(category2);
            driver.FindElement(SaveContactButton).Click();
            wait.Until(driver => CommonElements.Appears(driver, ContactDuplicateMessage));
            try
            {
                if (driver.FindElement(ContactDuplicateMessage).Text.Contains("This contact may be a duplicate of an existing contact. You may either click on Save to continue creating this new contact with the previously entered data or you may click Cancel."))
                    driver.FindElement(ContactDuplicateSaveButton).Click();
            }
            catch { }

        }

        private void AddCategory(string category)
        {
            driver.FindElement(CategoryInput).Click();
            driver.FindElement(CategorySearchList).Click();
            driver.FindElement(CategorySearchList).SendKeys(category);
            driver.FindElement(CategorySearchList).SendKeys(Keys.Enter);
        }
    }
}
