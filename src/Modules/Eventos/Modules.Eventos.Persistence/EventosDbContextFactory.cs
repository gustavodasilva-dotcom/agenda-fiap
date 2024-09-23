using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Modules.Eventos.Persistence;

public class EventosDbContextFactory : IDesignTimeDbContextFactory<EventosDbContext>
{
    public EventosDbContext CreateDbContext(string[] args)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<EventosDbContext>();
        var connectionString = configuration.GetConnectionString("Default");

        optionsBuilder.UseSqlServer(connectionString);

        var services = new ServiceCollection();

        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        var serviceProvider = services.BuildServiceProvider();

        var publisher = serviceProvider.GetRequiredService<IPublisher>();

        return new EventosDbContext(optionsBuilder.Options, publisher);
    }
}
