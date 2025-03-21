using OpenQA.Selenium;


namespace Mercator_SauceDemoTask.Pages
{
    public class CartPage
    {
        private readonly IWebDriver _driver;

        public CartPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool IsItemInCart()
        {
            try
            {
                var cartCount = _driver.FindElement(By.ClassName("shopping_cart_badge")).Text;
                return cartCount == "1";
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void NavigateToCart()
        {
            _driver.FindElement(By.ClassName("shopping_cart_badge")).Click();
        }

        public void RemoveProductFromCart(string productName)
        {
            var removeButton = _driver.FindElement(By.XPath($"//div[text()='{productName}']/ancestor::div[@class='cart_item']//button[text()='Remove']"));
            removeButton.Click();
        }
    }
}
