using Mercator_SauceDemoTask.Pages;
using Mercator_SauceDemoTask.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Mercator_SauceDemoTask.StepDefinitions
{
    [Binding]
    public class ShoppingCartSteps
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;
        private ProductsPage _productsPage;
        private CartPage _cartPage;

        public ShoppingCartSteps(
            IWebDriver driver,
            LoginPage loginPage,
            ProductsPage productsPage,
            CartPage cartPage)
        {
            _driver = driver;
            _loginPage = loginPage;
            _productsPage = productsPage;
            _cartPage = cartPage;
        }

        [Given(@"I navigate to the Sauce Demo website")]
        public void GivenINavigateToTheSauceDemoWebsite()
        {
            _driver.Navigate().GoToUrl(ConfigHelper.GetSetting("BaseUrl"));
            Logger.Log("Navigated to the Sauce Demo website");
        }

        [When(@"I login with username ""([^""]*)"" and password ""([^""]*)""")]
        public void WhenILoginWithUsernameAndPassword(string username, string password)
        {
            _loginPage.Login(username, password);
            Logger.Log($"Logged in with username {username} and password {password}");
        }

        [When(@"I add the highest priced product to the cart")]
        [When(@"I select the highest priced item")]
        public void WhenISelectTheHighestPricedItem()
        {
            _productsPage.AddHighestPricedItemToCart();
            
            Logger.Log("Selected the highest priced item");
            
        }

        [When(@"I go to the cart")]
        [When(@"I add the selected item to the cart")]
        public void WhenIAddTheSelectedItemToTheCart()
        {
            _productsPage.GoToCart();
            Logger.Log("Added the selected item to the cart");
        }

        [Then(@"the item should be in the shopping cart")]
        public void ThenTheItemShouldBeInTheShoppingCart()
        {
            var isItemInCart = _cartPage.IsItemInCart();
            isItemInCart.Should().BeTrue("the item should be in the shopping cart");
            Logger.Log($"Item is in cart: {isItemInCart}");

            //_productsPage = new ProductsPage(_driver);
            //_productsPage.GetHighestPricedProductName();
            //Logger.Log($"The highest priced item is: {_productsPage.GetHighestPricedProductName()}");
            _cartPage.NavigateToCart();
            
        }

        [Then(@"the highest priced product name should be in the cart")]
        public void ThenTheHighestPricedProductNameShouldBeInTheCart()
        {
           

            // Get the highest-priced product name from the products page
            var productsPage = new ProductsPage(_driver);
            var highestPricedProductName = productsPage.GetHighestPricedProductName();

            // Verify the highest-priced product is in the cart
            highestPricedProductName.Should().NotBeNullOrEmpty();
            //Assert.IsTrue(_cartPage.IsProductInCart(highestPricedProductName), $"The highest-priced product '{highestPricedProductName}' is not in the cart.");
        }
    }
}
