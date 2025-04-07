using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleCowApi.Data;
using SimpleCowApi.Data.Models;
using SimpleCowApi.Tests.Hooks;
using System;
using System.IO;
using System.Linq;

namespace SimpleCowApi.Tests.TestSetup
{
    public class TestApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string _testDbPath = $"TestDb_{Guid.NewGuid()}.db";

        public TestApplicationFactory()
        {

        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing DbContext registration
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Register test-specific SQLite DB
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite($"Data Source={_testDbPath}"));

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            });
        }
    }
}
