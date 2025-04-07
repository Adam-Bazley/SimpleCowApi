using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Reqnroll;
using SimpleCowApi.Data.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleCowApi.Tests.Steps
{
    [Binding]
    public class AddCowSteps
    {
        private readonly TestContext _context = new();
        private HttpResponseMessage _response;
        private int _seededFarmId;
        private int _featureFileFarmId;

        public AddCowSteps(ScenarioContext scenarioContext)
        {
            // Retrieve the shared farmId from ScenarioContext
            _seededFarmId = (int)scenarioContext["FarmId"];

        }

        [Given(@"the farm with ID (.*) exists")]
        public async Task GivenTheFarmWithIDExists(int farmId)
        {
            _featureFileFarmId = farmId;

            // Try to get the farm to check if it exists
            _response = await _context.Client.GetAsync($"/api/farms/{farmId}");

            if (_response.IsSuccessStatusCode)
            {
                // Farm exists, confirm it
                var existingFarm = await _response.Content.ReadFromJsonAsync<Farm>();
                Assert.That(existingFarm.Id, Is.EqualTo(farmId), "Farm already exists.");
            }
            else if (_response.StatusCode == HttpStatusCode.NotFound)
            {
                // Farm doesn't exist, create a new one
                var farm = new { id = farmId, name = "Test Farm", location = "Nowhere" };
                var content = new StringContent(JsonSerializer.Serialize(farm), Encoding.UTF8, "application/json");

                _response = await _context.Client.PostAsync("/api/farms", content);
                Assert.That(_response.IsSuccessStatusCode, Is.True, "Failed to create the farm.");
            }
            else
            {
                // Handle any other unexpected response
                Assert.Fail($"Unexpected response status: {_response.StatusCode}");
            }
        }


        [When(@"I add a cow named ""(.*)"" aged (.*) to the farm")]
        public async Task WhenIAddACowNamedAgedToTheFarm(string name, int age)
        {
            var cow = new { name = name, age = age, farmId = _featureFileFarmId };
            var content = new StringContent(JsonSerializer.Serialize(cow), Encoding.UTF8, "application/json");

            _response = await _context.Client.PostAsync("/api/cows", content);
        }

        [Then(@"the cow should be successfully added")]
        public void ThenTheCowShouldBeSuccessfullyAdded()
        {
            Assert.That(_response.IsSuccessStatusCode, Is.True, "Failed to add the cow.");
        }
    }
}
