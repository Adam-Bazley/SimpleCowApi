using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reqnroll;
using SimpleCowApi.Data;
using SimpleCowApi.Data.Models;
using System;
using System.Linq;

namespace SimpleCowApi.Tests.Hooks
{
    [Binding]
    public sealed class ExampleHooks
    {
        private static IServiceScope _scope;
        private static ApplicationDbContext _db;
        private static Random _random = new Random();

        private readonly ScenarioContext _scenarioContext;

        public ExampleHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            var scenarioName = _scenarioContext.ScenarioInfo.Title;

            var factory = new TestSetup.TestApplicationFactory();
            _scope = factory.Services.CreateScope();
            _db = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            SeedTestData();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _db?.Dispose();
            _scope?.Dispose();
        }

        private void SeedTestData()
        {
            if (!_db.Farms.Any())
            {
                // Generate a random Farm ID between 10000 and 99999
                int randomFarmId = _random.Next(10000, 99999);
                // Store the farmId in ScenarioContext to make it accessible across steps
                _scenarioContext["FarmId"] = randomFarmId;

                _db.Farms.Add(new Farm
                {
                    Id = randomFarmId,
                    Name = "Seeded Test Farm",
                    Location = "Testville"
                });

                _db.SaveChanges();
            }
        }
    }
}
