using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace Mercator_SauceDemoTask.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;

        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        //Locators
        private IWebElement ShoppingCartIcon => _driver.FindElement(By.ClassName("shopping_cart_link"));
        private IList<IWebElement> ProductPriceElements => _driver.FindElements(By.ClassName("inventory_item_price"));


        public void AddHighestPricedItemToCart()
        {
            var items = ProductPriceElements;
            decimal maxPrice = 0;
            IWebElement selectedItem = null;

            foreach (var item in items)
            {
                var priceText = item.Text.Replace("$", "");
                var price = decimal.Parse(priceText);
                if (price > maxPrice)
                {
                    maxPrice = price;
                    selectedItem = item.FindElement(By.XPath("ancestor::div[@class='inventory_item']"));
                }
            }

            selectedItem?.FindElement(By.ClassName("btn_inventory")).Click();
        }

        public void GoToCart()
        {
            ShoppingCartIcon.Click();
        }
        public string GetHighestPricedProductName()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));

            // Ensure inventory items are present
            wait.Until(ExpectedConditions.ElementExists(By.ClassName("inventory_item")));


            var items = ProductPriceElements;
            if (!items.Any())
            {
                throw new Exception("No price elements found on the page.");
            }

            decimal maxPrice = 0;
            IWebElement highestPricedItem = null;

            foreach (var item in items)
            {
                var priceText = item.Text.Replace("$", "");
                if (decimal.TryParse(priceText, out var price) && price > maxPrice)
                {
                    maxPrice = price;
                    highestPricedItem = item.FindElement(By.XPath("./parent::div[contains(@class, 'inventory_item')]"));
                }
            }

            if (highestPricedItem == null)
            {
                throw new Exception("No item found with a valid price.");
            }

            // Wait for the name element inside highestPricedItem
            var nameElement = wait.Until(driver =>
            {
                var element = highestPricedItem.FindElement(By.ClassName("inventory_item_name"));
                return element.Displayed ? element : null;
            });

            return nameElement.Text;
        }


    }
}
