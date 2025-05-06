using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using BoDi; 

namespace TodoMVC.Tests
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer _container;

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            ChromeOptions options = new ChromeOptions();
            // options.AddArgument("--headless"); // Uncomment for headless execution
            options.AddArgument("--start-maximized");

            IWebDriver driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            _container.RegisterInstanceAs<IWebDriver>(driver); 
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_container.IsRegistered<IWebDriver>())
            {
                 var driver = _container.Resolve<IWebDriver>(); 

                 driver?.Quit(); 
                 driver?.Dispose();
            }
        }
    }
}