Feature: Add Cow
  As a farmer
  I want to add a cow to my farm
  So that it is registered in the system

  Scenario: Add a new cow to a farm
    Given the farm with ID 90001 exists
    When I add a cow named "Buttercup" aged 3 to the farm
    Then the cow should be successfully added
