Feature: ShoppingCart

As a user I want to add the higgest priced item to my shopping cart that I can purchase it


Scenario: Add highest priced item to shopping cart
 Given I navigate to the Sauce Demo website  
 When I login with username "standard_user" and password "secret_sauce"
 And I select the highest priced item
 And I add the selected item to the cart
 Then the item should be in the shopping cart
