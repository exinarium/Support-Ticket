using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SupportTicket.Client;
using SupportTicket.Client.Config;
using SupportTicket.Client.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.Configure<ClientConfig>(
    builder.Configuration.GetSection("ClientConfig"));

builder.Services.AddHttpClientFactory();
builder.Services.AddAuthorization();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();