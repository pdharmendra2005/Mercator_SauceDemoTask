Feature: ShoppingCart

As a user I want to add the higgest priced item to my shopping cart that I can purchase it


Scenario: Add highest priced item to shopping cart
 Given I navigate to the Sauce Demo website  
 When I login with username "standard_user" and password "secret_sauce"
 And I select the highest priced item
 And I add the selected item to the cart
 Then the item should be in the shopping cart




Scenario: Verify the highest-priced product name in the cart
  Given I navigate to the Sauce Demo website
  When I login with username "standard_user" and password "secret_sauce"
  And I add the highest priced product to the cart
  And I go to the cart
  Then the highest priced product name should be in the cart


Scenario: Remove a product from the cart
  Given I navigate to the Sauce Demo website
  When I login with username "standard_user" and password "secret_sauce"
  And I add the product "Sauce Labs Backpack" to the cart
  And I go to the cart
  And I remove the product "Sauce Labs Backpack" from the cart
  Then the product "Sauce Labs Backpack" should not be in the cart


Scenario: Checkout flow
  Given I navigate to the Sauce Demo website
  When I login with username "standard_user" and password "secret_sauce"
  And I select the highest priced item
  And I go to the cart
  And I proceed to checkout
  And I enter my checkout information
  And I complete the checkout
  Then the order should be completed successfully
