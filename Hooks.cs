using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using BoDi; // Required for dependency injection

// Ensure this namespace matches your test project's default namespace if needed
// (Usually the project folder name: TodoMVC.Tests)
namespace TodoMVC.Tests
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer _container;

        // Constructor for SpecFlow dependency injection
        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            // Configure WebDriver options
            ChromeOptions options = new ChromeOptions();
            // options.AddArgument("--headless"); // Uncomment for headless execution
            options.AddArgument("--start-maximized");

            // Initialize WebDriver
            IWebDriver driver = new ChromeDriver(options);

            // Set implicit wait (optional but often helpful)
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Register the WebDriver instance with SpecFlow's container
            // This makes it available to step definition constructors
            _container.RegisterInstanceAs<IWebDriver>(driver); // Ensure semicolon is present
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Retrieve the WebDriver instance from the container
            // Use TryResolve to avoid errors if registration failed
            if (_container.IsRegistered<IWebDriver>())
            {
                 var driver = _container.Resolve<IWebDriver>(); // Ensure semicolon is present

                 // Quit the driver and dispose of the browser instance
                 driver?.Quit(); // Safely call Quit()
                 driver?.Dispose(); // Safely call Dispose()
            }
        }
    }
}