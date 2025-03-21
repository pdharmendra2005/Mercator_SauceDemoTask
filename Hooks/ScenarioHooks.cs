using BoDi;
using Mercator_SauceDemoTask.Pages;
using Mercator_SauceDemoTask.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace Mercator_SauceDemoTask.Hooks
{
    [Binding]
    public class ScenarioHooks
    {
        private IWebDriver _driver;
        
        private readonly IObjectContainer _objectContainer;

        public ScenarioHooks(IObjectContainer objectContainer)
        {
            
           
            _objectContainer = objectContainer;
        }

        [BeforeScenario(Order = 0)]
        public void RegisterDependencies()
        {
            var browser = ConfigHelper.GetSetting("Browser");
            var headless = ConfigHelper.GetBoolSetting("Headless");

            switch (browser)
            {
                case "Chrome":
                    var chromeOptions = new ChromeOptions();
                    if (headless)
                    {
                        chromeOptions.AddArgument("--headless");
                    }
                    _driver = new ChromeDriver(chromeOptions);
                    break;

                case "Firefox":
                    var firefoxOptions = new FirefoxOptions();
                    if (headless)
                    {
                        firefoxOptions.AddArgument("--headless");
                    }
                    _driver = new FirefoxDriver(firefoxOptions);
                    break;

                case "Edge":
                    var edgeOptions = new EdgeOptions();
                    if (headless)
                    {
                        edgeOptions.AddArgument("--headless");
                    }
                    _driver = new EdgeDriver(edgeOptions);
                    break;

                default:
                    throw new ArgumentException($"Unsupported browser: {browser}");
            }

            _driver.Manage().Window.Maximize();
         //   _scenarioContext["driver"] = _driver;

            //Register page objects dependencies
            _objectContainer.RegisterInstanceAs(_driver);

            _objectContainer.RegisterTypeAs<LoginPage, LoginPage>();
            _objectContainer.RegisterTypeAs<ProductsPage, ProductsPage>();
            _objectContainer.RegisterTypeAs<CartPage, CartPage>();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_driver != null)
            {
                Thread.Sleep(2000);
                _driver.Quit();
            }
        }
    }
}
