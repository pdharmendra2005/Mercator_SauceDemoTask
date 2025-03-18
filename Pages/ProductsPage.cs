using OpenQA.Selenium;


namespace Mercator_SauceDemoTask.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;

        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void AddHighestPricedItemToCart()
        {
            var items = _driver.FindElements(By.ClassName("inventory_item_price"));
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

       
    }
}
