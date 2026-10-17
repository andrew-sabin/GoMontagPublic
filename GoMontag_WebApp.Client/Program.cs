using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Captcha.ReCaptcha;
using BlazorSortable;
using EasyAppDev.Blazor.AutoComplete.Extensions;
using GoMontag.Shared.Helpers;
using GoMontag_Shared.Helpers;
using GoMontag_WebApp.Client;
using GoMontag_WebApp.Client.ClientComponentServices;
using GoMontag_WebApp.Client.ClientComponentServices.Interface;
using GoMontag_WebApp.Client.Helpers;
using GoMontag_WebApp.Client.Pages;
using GoogleCaptchaComponent;
using GoogleCaptchaComponent.Configuration;
using GoogleCaptchaComponent.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sqids;
using Syncfusion.Blazor;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("");


var builder = WebAssemblyHostBuilder.CreateDefault(args);

// White/Allow List Section
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.Configure<UrlWhitelistOptions>(
    builder.Configuration.GetSection("UrlWhiteList"));

builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddHttpClient("ComponentApi", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<LinkProcessor>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ICategoryClientToServerComponentService, CategoryClientComponentService>();
builder.Services.AddScoped<IAuthClientComponentService, AuthClientService>();
builder.Services.AddScoped<IEmailClientComponentService, EmailClientComponentService>();
builder.Services.AddScoped<IUserTimeZoneClientService, UserTimeZoneClientService>();

builder.Services.AddScoped<IProductClientToServerComponentService, ProductClientToServerComponentService>();
builder.Services.AddScoped<IAvailabilityClientToServerComponentService, AvailabilityClientToServerComponentService>();
builder.Services.AddScoped<IStoreClientToServerComponentService, StoreClientToServerComponentService>();
builder.Services.AddScoped<IStoreWASMComponentService, StoreWASMComponentService>();
builder.Services.AddScoped<IFormatClientToServerComponentService, FormatClientToServerComponentService>();
builder.Services.AddScoped<IFormatImageClientToServerComponentService, FormatImageClientToServerComponentService>();
builder.Services.AddScoped<IFormatTypeClientToServerComponentService, FormatTypeClientToServerComponentService>();
builder.Services.AddScoped<IFormatRevisionClientToServerComponentService, FormatRevisionClientToServerComponentService>();
builder.Services.AddScoped<INewsBlogClientToServerComponentService, NewsBlogClientToServerComponentService>();
builder.Services.AddScoped<IUserClientToServerComponentService, UserClientToServerComponentService>();
builder.Services.AddScoped<IRecommendationItemClientToServerComponentService, RecommendationItemClientToServerComponentService>();
builder.Services.AddScoped<IRecommendationListClientToServerComponentService, RecommendationListClientToServerComponentService>();

builder.Services.AddScoped<IAuthClientToServerComponentService, AuthClientToServerService>();
builder.Services.AddScoped<ILoginClientToServerComponentService, LoginClientToServerComponentService>();

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddAuthorizationCore();


builder.Services.AddSingleton(new SqidsEncoder<int>(new()
{
    MinLength = 0,
    Alphabet = "",
}));

builder.Services.AddSyncfusionBlazor();

builder.Services.AddGoogleCaptcha(configuration => {
    configuration.V2SiteKey = "";
    configuration.DefaultVersion = CaptchaConfiguration.Version.V2;
    configuration.DefaultTheme = CaptchaConfiguration.Theme.Light;
    configuration.DefaultLanguage = CaptchaLanguages.English;
});

builder.Services.AddAutoComplete();
builder.Services.AddSortable();

builder.Services.AddTransient<AuthMessageHandler>();
builder.Services.AddHttpClient<AuthClientService>()
    .AddHttpMessageHandler<AuthMessageHandler>();

await builder.Build().RunAsync();

