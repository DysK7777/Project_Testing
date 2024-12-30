Feature: Access Control

  Scenario: Active user within working hours
    Given the user "12345" is active
    And the current time is "10:00"
    When the user attempts to access the building
    Then access should be granted

  Scenario: Inactive user
    Given the user "67890" is inactive
    And the current time is "10:00"
    When the user attempts to access the building
    Then access should be denied

  Scenario: Active user outside working hours
    Given the user "12345" is active
    And the current time is "20:00"
    When the user attempts to access the building
    Then access should be denied

  Scenario: Unknown access card
    Given the user "99999" is unknown
    And the current time is "10:00"
    When the user attempts to access the building
    Then access should be denied