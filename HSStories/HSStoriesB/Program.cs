using Blazored.LocalStorage;
using HSStoriesB;
using HSStoriesB.Provider;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7195/")
    });

builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddScoped<AuthenticationStateProvider, AuthState>();
builder.Services.AddSingleton<AuthState>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();



await builder.Build().RunAsync();
