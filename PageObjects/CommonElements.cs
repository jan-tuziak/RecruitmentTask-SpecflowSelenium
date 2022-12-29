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
    /// <summary>
    /// UI Elements shared across pages
    /// </summary>
    public class CommonElements
    {
        //The Selenium web driver to automate the browser
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly int defaultTimeout = 15;

        public CommonElements(IWebDriver webDriver)
        {
            driver = webDriver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(defaultTimeout));
        }

        private By StatusText => By.Id("ajaxStatusDiv");

        public void WaitForStatusToDisappear()
        {
            //Wait until Status Text is invisible
            wait.Until(driver => Exists(driver, StatusText));
            wait.Until(driver => !driver.FindElement(StatusText).Displayed);
        }

        public static bool Exists(IWebDriver driver, By locator) => 
            driver.FindElements(locator).Count > 0;

        public static bool Appears(IWebDriver driver, By locator) => 
            Exists(driver, locator) && driver.FindElement(locator).Displayed;
    }
}
