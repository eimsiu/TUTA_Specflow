Feature: PostCodeTest
	As a UK citizen
	I want a postcode lookup
	Because I want to find out more information about it

Background: 
	Given I am using the base url 'http://api.postcodes.io/postcodes/' value

Scenario Outline: As a Service I validate values in API Response
	Given I setup the request to GET using the provided '<postCode>' value
	When I send the request
	Then I should receive a response
	And I should receive a status code equal <responseCode>
	And I validate country should have <country> value
	And I validate that geographic Cartesian coordinates have <eastings> value and <northings> value
	And I validate that codes.admin_district has value '<code_adminDistrict>'
	And I validate that '<ResponseObjectValue>' has value of either nhs_ha, msoa or lsoa


	Examples: 
	| postCode | responseCode | country | eastings | northings | code_adminDistrict | responseObject | ResponseObjectValue      | ObjectTwo  | ObjectValueTwo         | ObjectThree| ObjectValueThree     |
	| LS3 1EP  | 200          | England | 429320   | 433751    | E08000035          | nhs_ha         | Yorkshire and the Humber | lsoa       | Leeds 063B             | msoa       | Leeds 063            |
	| NR34 2PF | 404          | error   | error    | error     | error              | error          | error                    | error      | error                  | error      | error                |
	| OX49 5NU | 200          | England | 464438   | 195677    | E07000179          | nhs_ha         | South Central            | lsoa       | South Oxfordshire 011B | msoa       | South Oxfordshire 011|
	| M32 0JG  | 200          | England | 379988   | 395476    | E08000009          | nhs_ha         | North West               | lsoa       | Trafford 003C          | msoa       | Trafford 003         |
	| NE30 1DP | 200          | England | 435958   | 568671    | E08000022          | nhs_ha         | North East               | lsoa       | North Tyneside 016C    | msoa       | North Tyneside 016   |

# tasks:
#	
#	1) Create an assertion that checks if the returned status code is 200 (use HttpResponseMessage.StatusCode )

#	2) Create an assertion that would validate: country
#	3) Create an assertion that would validate: eastings, northings
#	4) Create an assertion that would validate: codes.admin_district

#   

#	5) Complete the examples table with more test cases 
#		- it MUST contain a negative test
#		- there should be atleast 4 test cases (this is with the negative test case)

#	6) Add an additional assertion that validates either: nhs_ha, lsoa, msoa
#		e.g. responseObject has the field which will be checked, ResponseObjectValue has the value it's tested against (3 different object)
#
#	| postCode	| responseObject | ResponseObjectValue		|
#	| LS3 1EP 	| nhs_ha		 | Yorkshire and the Humber |
#	| LS3 1EP	| longitude      | Leeds 063B				|
#	| LS3 1EP	| latitude       | Leeds 063				|  


# Notes: 
# Good postcodes: LS3 1EP, NR34 2PF, OX49 5NU, M32 0JG, NE30 1DP

# The example's first test case has everything filled for tasks 1-5, you still need to add columns for task 6 thou!

# For task 6, is recomendet to use a switch statement rather than multiple nested if statements
# https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/switch
# https://www.csharp-examples.net/switch/