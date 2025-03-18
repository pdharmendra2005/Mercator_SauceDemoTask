using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Mercator_SauceDemoTask.Hooks
{
    [Binding]
    public class ScenarioHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public ScenarioHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.ContainsKey("WebDriver"))
            {
                var driver = _scenarioContext.Get<IWebDriver>("WebDriver");
                Thread.Sleep(2000);
                driver.Quit();
            }
        }
    }
}
