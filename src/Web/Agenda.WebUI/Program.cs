using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Agenda.WebUI.Models;
using Agenda.WebUI.Services;
using Agenda.WebUI;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<ContatosService>();
builder.Services.AddScoped<EventosService>();
builder.Services.AddRadzenComponents();

var uriString = builder.Configuration.GetSection("API").Value;

builder.Services.AddHttpClient(
    HttpClientNames.MyApiContatos,
    x =>
    {
        x.BaseAddress = new Uri(builder.Configuration.GetSection("API").Value);
    }
);

await builder.Build().RunAsync();
