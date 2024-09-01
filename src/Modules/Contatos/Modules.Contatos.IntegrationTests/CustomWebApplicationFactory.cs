using Agenda.App;
using Agenda.Modules.Contatos.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using MediatR;
using System.Linq;
using Agenda.Common.Shared.Abstractions;

internal sealed partial class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureServices(services => {
            // Remove the existing DbContext registration
            var descriptor = services.SingleOrDefault(d
                => d.ServiceType == typeof(DbContextOptions<ContatosDbContext>));

            if (descriptor != null) {
                services.Remove(descriptor);
            }

            // Add DbContext using in-memory database for testing
            services.AddDbContext<ContatosDbContext>(options => {
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

            var db = scopedServices.GetRequiredService<ContatosDbContext>();

            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory>>();

            try {
                // Ensure the database is created
                db.Database.EnsureCreated();
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred creating the in-memory database. Error: {Message}", ex.Message);
                throw;
            }
        });
    }
}
