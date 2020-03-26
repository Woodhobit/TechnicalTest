Feature: UserProfile
	In order to set up a new user account 
	As a client application
	I want to be able to create and update a user profile

Scenario: Create new user
    Given I am a client
    When I make a post request to "api/users" with the following data "{ 'firstName' : 'Dummy first name','secondName':'Dummy second name'','email':'testg@test.com'}"
    Then the response status code is "200"
    And the response data should be "{ id: 1 }"

Scenario: Fail to create new user with empty Email address
    Given I am a client
    When I make a post request to "api/users" with the following data "{ 'firstName' : 'Dummy first name','secondName':'Dummy second name'','email':''}"
    Then the response status code is "401"

 Scenario: Fail to update the user with empty Email address
    Given I am a client
    When I make a post request to "api/users" with the following data "{ 'id' = 1, 'firstName' : 'Dummy first name','secondName':'Dummy second name'','email':''}"
    Then the response status code is "401"

 Scenario: Fail to update the not not existed
    Given I am a client
    When I make a post request to "api/users" with the following data "{ 'id' = 404, 'firstName' : 'Dummy first name','secondName':'Dummy second name'','email':''}"
    Then the response status code is "404"
