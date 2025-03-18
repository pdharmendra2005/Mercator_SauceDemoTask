using Mercator_SauceDemoTask.Pages;
using Mercator_SauceDemoTask.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Mercator_SauceDemoTask.StepDefinitions
{
    [Binding]
    public class ShoppingCartSteps
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private LoginPage _loginPage;
        private ProductsPage _productsPage;
        private CartPage _cartPage;

        public ShoppingCartSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I navigate to the Sauce Demo website")]
        public void GivenINavigateToTheSauceDemoWebsite()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _scenarioContext.Set(_driver, "WebDriver");
            _driver.Navigate().GoToUrl(ConfigHelper.GetSetting("BaseUrl"));
            Logger.Log("Navigated to the Sauce Demo website");
        }

        [When(@"I login with username ""([^""]*)"" and password ""([^""]*)""")]
        public void WhenILoginWithUsernameAndPassword(string username, string password)
        {
            _loginPage = new LoginPage(_driver);
            _loginPage.Login(username, password);
            Logger.Log($"Logged in with username {username} and password {password}");
        }

        [When(@"I select the highest priced item")]
        public void WhenISelectTheHighestPricedItem()
        {
            _productsPage = new ProductsPage(_driver);
            _productsPage.AddHighestPricedItemToCart();
            Logger.Log("Selected the highest priced item");
        }

        [When(@"I add the selected item to the cart")]
        public void WhenIAddTheSelectedItemToTheCart()
        {
            Logger.Log("Added the selected item to the cart");
        }

        [Then(@"the item should be in the shopping cart")]
        public void ThenTheItemShouldBeInTheShoppingCart()
        {
            _cartPage = new CartPage(_driver);
            var isItemInCart = _cartPage.IsItemInCart();
            _cartPage.NavigateToCart();
            Logger.Log($"Item is in cart: {isItemInCart}");
        }
    }
}
