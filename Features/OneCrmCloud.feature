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
	#And I create a new contact 
	#And The first name is 'Jan' 
	#And I enter last name 'Kamykowski'
	#And I assign role 'Admin' 
	#And I enter category 'Customers'
	#And I enter category 'Suppliers'
	#When I click save contact
	#Then The new contact first name is 'Jan' 
	#Then The new contact last name is 'Kamykowski' 
	#Then The new contact role 'Admin' 
	#Then The new contact category is 'Customers' 
	#Then The new contact category is 'Suppliers' 