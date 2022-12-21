using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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

        public ActivityLogPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(DefaultWaitInSeconds));
        }

        //Finding elements by ID
        //Get all checkboxes from the table and remove the first one (the one in the header)
        private List<IWebElement> checkboxes => _webDriver.FindElements(By.CssSelector("div.input-check>input.checkbox")).ToList().Skip(1).ToList();
        private IWebElement numberOfAllItems => _webDriver.FindElement(By.CssSelector("div#content-main>div>div>div:nth-child(2)>div>div:nth-child(2)>span:nth-child(2)"));
        private IWebElement ActionsButton => _webDriver.FindElement(By.CssSelector("div#content-main>div>div>div:nth-child(2)>div>div:first-child>div:first-child>button:first-child"));
        private IWebElement ActionDeleteButton => _webDriver.FindElement(By.CssSelector("div[id$=ActionButtonHead-popup]>div:first-child>div:first-child>div:nth-child(2)"));
        private int numberOfSelectedItems;
        private int numberOfItemsBeforeDeletion;

        internal void SelectFirstItemsInTheTable(int numberOfItemsToSelect)
        {
            for (int i = 0; i < numberOfItemsToSelect; i++)
            {
                Thread.Sleep(250);
                checkboxes[i].Click();
            }
            numberOfSelectedItems = numberOfItemsToSelect;
        }

        internal void DeleteSelectedItems()
        {
            this.numberOfItemsBeforeDeletion = Int32.Parse(numberOfAllItems.Text, System.Globalization.NumberStyles.AllowThousands);
            
            ActionsButton.Click();
            Thread.Sleep(500);
            ActionDeleteButton.Click();
            Thread.Sleep(1000);
            _webDriver.SwitchTo().Alert().Accept();
            Thread.Sleep(1500);
        }

        internal void CheckIfItemsWereDeleted()
        {
            int numberOfItemsAfterDeletion = Int32.Parse(numberOfAllItems.Text, System.Globalization.NumberStyles.AllowThousands);

            numberOfItemsAfterDeletion.Should().Be(numberOfItemsBeforeDeletion - numberOfSelectedItems);
        }
    }
}
