Feature: GoIndigoBooking
Background: 
	Given the goindigo home page is open
@mytag
Scenario: Book Flight Tickets
	Given the number of passengers is 2
	And moving from 'Pune'
	And moving to 'Delhi'
	And date of journey is '01/feb/2021'
	When I click search flight
	Then I should get search results