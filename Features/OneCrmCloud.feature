Feature: OneCrmCloud
	Testing demo.1crmcloud.com site

@1CrmCloud
Scenario: Log In and Out
	Given I login
	Then I logout

@1CrmCloud
Scenario: Create Contact
	Given I login
	And I navigate to 'Contacts under Sales & Marketing'
	And I click create new contact
	When I create a new contact 'Jan' 'Kamykowski' 'Admin' 'Customers' 'Suppliers'
	Then The new contact is 'Jan' 'Kamykowski' 'Admin' 'Customers' 'Suppliers'

@1CrmCloud
Scenario: Run Report
	Given I login
	And I navigate to 'Reports under Reports & Settings'
	And I pick report 'Project Profitability'
	When I run the report 'Project Profitability'
	Then I get some results for 'Project Profitability'