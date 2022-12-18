using RecruitmentTaskSpecflowSelenium.Drivers;
using RecruitmentTaskSpecflowSelenium.PageObjects;
using System;
using TechTalk.SpecFlow;

namespace RecruitmentTaskSpecflowSelenium.StepDefinitions
{
    [Binding]
    public class OneCrmCloudStepDefinitions
    {
        private readonly LoginPageObject _loginPageObject;
        private readonly HomePageObject _homePageObject;

        public OneCrmCloudStepDefinitions(BrowserDriver browserDriver)
        {
            _loginPageObject = new LoginPageObject(browserDriver.Current);
            _homePageObject = new HomePageObject(browserDriver.Current);
        }

        [Then(@"I logout")]
        public void ThenILogout()
        {
            _homePageObject.Logout();
        }

        [When(@"I click save contact")]
        public void WhenIClickSaveContact()
        {
            throw new PendingStepException();
        }

        [Given(@"I create a new contact")]
        public void GivenICreateANewContact()
        {
            throw new PendingStepException();
        }

        [Given(@"I login")]
        public void GivenILogin()
        {
            //Delegate to Page Object
            _loginPageObject.LoginAsAdmin();
        }

        [Given(@"I navigate to '([^']*)'")]
        public void GivenINavigateTo(string tabName)
        {
            _homePageObject.NavigateTo(tabName);
        }

        [Given(@"The first name is '([^']*)'")]
        public void GivenTheFirstNameIs(string firstName)
        {
            throw new PendingStepException();
        }

        [Given(@"I enter last name '([^']*)'")]
        public void GivenIEnterLastName(string lastName)
        {
            throw new PendingStepException();
        }

        [Given(@"I assign role '([^']*)'")]
        public void GivenIAssignRole(string role)
        {
            throw new PendingStepException();
        }

        [Given(@"I enter category '([^']*)'")]
        public void GivenIEnterCategory(string category)
        {
            throw new PendingStepException();
        }

        [Then(@"The new contact first name is '([^']*)'")]
        public void ThenTheNewContactFirstNameIs(string firstName)
        {
            throw new PendingStepException();
        }

        [Then(@"The new contact last name is '([^']*)'")]
        public void ThenTheNewContactLastNameIs(string lastName)
        {
            throw new PendingStepException();
        }

        [Then(@"The new contact role '([^']*)'")]
        public void ThenTheNewContactRole(string role)
        {
            throw new PendingStepException();
        }

        [Then(@"The new contact category is '([^']*)'")]
        public void ThenTheNewContactCategoryIs(string category)
        {
            throw new PendingStepException();
        }

    }
}
