using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTaskSpecflowSelenium.PageObjects
{
    internal class ActivityLogPageObject
    {
        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 5;

        WebDriverWait wait;

        CommonElements ce;

        public ActivityLogPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
            ce = new CommonElements(_webDriver);
        }

        //Finding elements by ID
        private By checkboxes => By.CssSelector("div.input-check>input.checkbox");
        private By numberOfAllItems => By.CssSelector("div#content-main>div>div>div:nth-child(2)>div>div:nth-child(2)>span:nth-child(2)");
        private By ActionsButton => By.CssSelector("div#content-main>div>div>div:nth-child(2)>div>div:first-child>div:first-child>button:first-child");
        private By ActionDeleteButton => By.CssSelector("div[id$=ActionButtonHead-popup]>div:first-child>div:first-child>div:nth-child(2)");
        private int numberOfSelectedItems;
        private int numberOfItemsBeforeDeletion;

        internal void SelectFirstItemsInTheTable(int numberOfItemsToSelect)
        {
            ce.WaitForStatusToDisappear();
            //Get all checkboxes from the table and remove the first one (the one in the header)
            var checks = _webDriver.FindElements(checkboxes).ToList().Skip(1).ToList();
            for (int i = 0; i < numberOfItemsToSelect; i++)
            {
                checks[i].Click();
            }
            numberOfSelectedItems = numberOfItemsToSelect;
        }

        internal void DeleteSelectedItems()
        {
            this.numberOfItemsBeforeDeletion = Int32.Parse(_webDriver.FindElement(numberOfAllItems).Text, System.Globalization.NumberStyles.AllowThousands);

            _webDriver.FindElement(ActionsButton).Click();
            
            wait.Until(driver => CommonElements.Appears(driver, ActionDeleteButton));
            _webDriver.FindElement(ActionDeleteButton).Click();

            wait.Until(ExpectedConditions.AlertIsPresent());
            _webDriver.SwitchTo().Alert().Accept();
            
            ce.WaitForStatusToDisappear();
        }

        internal void CheckIfItemsWereDeleted()
        {
            int numberOfItemsAfterDeletion = Int32.Parse(_webDriver.FindElement(numberOfAllItems).Text, System.Globalization.NumberStyles.AllowThousands);

            numberOfItemsAfterDeletion.Should().Be(numberOfItemsBeforeDeletion - numberOfSelectedItems);
        }
    }
}
