using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebUI.Models;
using WebUI.Services;
using WebUIAgenda;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<ContatosService>();

var uriString = builder.Configuration.GetSection("API").Value;

builder.Services.AddHttpClient(
    HttpClientNames.MyApiContatos,
    x =>
    {
        x.BaseAddress = new Uri(builder.Configuration.GetSection("API").Value);
    }
);

await builder.Build().RunAsync();
