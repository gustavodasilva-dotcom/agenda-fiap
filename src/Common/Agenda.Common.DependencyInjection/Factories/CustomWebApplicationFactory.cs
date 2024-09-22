using Agenda.Common.Shared.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace Agenda.Common.DependencyInjection.Factories;

public abstract class CustomWebApplicationFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>
    where TProgram : class
    where TDbContext : DbContext
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configurator) =>
        {
            var inMemorySettings = new Dictionary<string, string?>
            {
                { "MessageBroker:Host", "localhost" },
                { "MessageBroker:Username", "guest" },
                { "MessageBroker:Password", "guest" },
                { "MessageBroker:NumberOfRetries", "3" },
                { "MessageBroker:KillSwitch:ActivationThreshold", "5" },
                { "MessageBroker:KillSwitch:TripThreshold", "0.5" },
                { "MessageBroker:KillSwitch:RestartMinutesTimeout", "10" }
            };
            configurator.AddInMemoryCollection(inMemorySettings);
        });

        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext registration
            var descriptor = services.SingleOrDefault(d
                => d.ServiceType == typeof(DbContextOptions<TDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add DbContext using in-memory database for testing
            services.AddDbContext<TDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Adicionar o mock de IPublisher
            var mockPublisher = new Mock<IPublisher>();
            mockPublisher
                .Setup(p => p.Publish(It.IsAny<IDomainEvent>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            services.AddSingleton<IPublisher>(mockPublisher.Object);

            // Build the service provider
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database contexts
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;

            var db = scopedServices.GetRequiredService<TDbContext>();

            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram, TDbContext>>>();

            try
            {
                // Ensure the database is created
                db.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred creating the in-memory database. Error: {Message}", ex.Message);
                throw;
            }
        });
    }
}
