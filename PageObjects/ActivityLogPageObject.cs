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
        private readonly IWebDriver driver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 5;

        WebDriverWait wait;

        CommonElements ce;

        public ActivityLogPageObject(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
            ce = new CommonElements(driver);
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
            var checks = driver.FindElements(checkboxes).ToList().Skip(1).ToList();
            for (int i = 0; i < numberOfItemsToSelect; i++)
            {
                checks[i].Click();
            }
            numberOfSelectedItems = numberOfItemsToSelect;
        }

        internal void DeleteSelectedItems()
        {
            this.numberOfItemsBeforeDeletion = Int32.Parse(driver.FindElement(numberOfAllItems).Text, System.Globalization.NumberStyles.AllowThousands);

            driver.FindElement(ActionsButton).Click();
            
            wait.Until(driver => CommonElements.Appears(driver, ActionDeleteButton));
            driver.FindElement(ActionDeleteButton).Click();

            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();
            
            ce.WaitForStatusToDisappear();
        }

        internal void CheckIfItemsWereDeleted()
        {
            int numberOfItemsAfterDeletion = Int32.Parse(driver.FindElement(numberOfAllItems).Text, System.Globalization.NumberStyles.AllowThousands);

            numberOfItemsAfterDeletion.Should().Be(numberOfItemsBeforeDeletion - numberOfSelectedItems);
        }
    }
}
