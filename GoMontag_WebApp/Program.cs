using BlazorSortable;
using EasyAppDev.Blazor.AutoComplete.Extensions;
using GoMontag.Shared.Configuration;
using GoMontag.Shared.Models.Account;
using GoMontag.Shared.Models.User;
using GoMontag_Shared.Entities;
using GoMontag_Shared.Entities.Revisions;
using GoMontag_Shared.Models.NewsUpdate;
using GoMontag_WebApp;
using GoMontag_WebApp.Client.ClientComponentServices;
using GoMontag_WebApp.Client.ClientComponentServices.Interface;
using GoMontag_WebApp.Client.Pages;
using GoMontag_WebApp.Components;
using GoMontag_WebApp.Helpers;
using GoMontag_WebApp.Middleware;
using GoMontag_WebApp.Repositories;
using GoMontag_WebApp.Repositories.Interfaces;
using GoMontag_WebApp.Scripts.Seeds;
using GoMontag_WebApp.ServerComponentServices;
using GoMontag_WebApp.ServerComponentServices.Interface;
using GoMontag_WebApp.Services;
using GoMontag_WebApp.Services.Interface;
using GoogleCaptchaComponent;
using GoogleCaptchaComponent.Configuration;
using GoogleCaptchaComponent.Models;
using Mapster;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi;
using Radzen;
using Swashbuckle.AspNetCore.Filters;
using Syncfusion.Blazor;
using System.Text;
using System.Text.Json.Serialization;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("");

var builder = WebApplication.CreateBuilder(args);

// White/Allow List Section
builder.Services.Configure<UrlWhitelistOptions>(
    builder.Configuration.GetSection("UrlWhiteList"));

builder.Services.AddCascadingAuthenticationState();

// Add support for Blazor WebAssembly files
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.UseInlineDefinitionsForEnums();
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserDbConnection")));

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedAccount = true; // Set to true for production

    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;

    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddHttpClient("ComponentApi", client =>
{
    string baseAddress = builder.Configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7168";
    client.BaseAddress = new Uri(baseAddress);
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

// Configure SMTP2GO
builder.Services.Configure<Smtp2GoSettings>(
    builder.Configuration.GetSection("Smtp2GoSettings"));

/* API Repos and Services */
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFormatRepository, FormatRepository>();
builder.Services.AddScoped<IFormatService, Format_Service>();
builder.Services.AddScoped<IFormatTypeRepository, FormatTypeRepository>();
builder.Services.AddScoped<IFormatTypeService, FormatTypeService>();
builder.Services.AddScoped<IFormatRevisionRepository, FormatRevisionRepository>();
builder.Services.AddScoped<IFormatRevisionService, FormatRevisionService>();
builder.Services.AddScoped<IFormatImagesRepository, FormatImageRepository>();
builder.Services.AddScoped<IFormatImageService, FormatImageService>();

builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<INewsBlogRepository, NewsBlogRepository>();
builder.Services.AddScoped<INewsBlogService, NewsBlogService>();

builder.Services.AddScoped<IRecommendationListRepo, RecommendationListRepo>();
builder.Services.AddScoped<IRecommendationListService, RecommendationListService>();
builder.Services.AddScoped<IRecommendationItemsRepo, RecommendationItemRepo>();
builder.Services.AddScoped<IRecommendationItemService, RecommendationItemService>();

builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthStateProvider>();

/* Component Services */
builder.Services.AddScoped<IProductComponentService, ProductComponentService>();
builder.Services.AddScoped<ICategoryComponentService, CategoryComponentService>();
builder.Services.AddScoped<IFormatComponentService, FormatComponentService>();
builder.Services.AddScoped<IFormatTypeComponentService, FormatTypeComponentService>();
builder.Services.AddScoped<IStoreComponentService, StoreComponentService>();
builder.Services.AddScoped<IAvailabilityComponentService, AvailabilityComponentService>();
builder.Services.AddScoped<IAuthComponentService, AuthService>();
builder.Services.AddScoped<IUserComponentService, UserComponentService>();
builder.Services.AddScoped<IEmailComponentService, EmailComponentService>();
builder.Services.AddScoped<INewsBlogComponentService, NewsBlogComponentService>();
builder.Services.AddScoped<IFormatRevisionComponentService, FormatRevisionComponentService>();
builder.Services.AddScoped<IUserTimeZoneService, UserTimeZoneService>();

builder.Services.AddScoped<IRecommendationItemComponentService, RecommendationItemComponentService>();
builder.Services.AddScoped<IRecommendationListComponentService, RecommendationListComponentService>();

/* Client to Server Component Services */
builder.Services.AddScoped<IAuthClientToServerComponentService, AuthClientToServerService>();
builder.Services.AddScoped<ILoginClientToServerComponentService, LoginClientToServerComponentService>();

builder.Services.AddScoped<IUserTimeZoneClientService, UserTimeZoneClientService>();
builder.Services.AddScoped<ICategoryClientToServerComponentService, CategoryClientToServerComponentService>();
builder.Services.AddScoped<IAvailabilityClientToServerComponentService, AvailabilityClientToServerComponentService>();
builder.Services.AddScoped<IProductClientToServerComponentService, ProductClientToServerComponentService>();
builder.Services.AddScoped<IFormatClientToServerComponentService, FormatClientToServerComponentService>();
builder.Services.AddScoped<IFormatRevisionClientToServerComponentService, FormatRevisionClientToServerComponentService>();
builder.Services.AddScoped<IFormatTypeClientToServerComponentService, FormatTypeClientToServerComponentService>();
builder.Services.AddScoped<IFormatImageClientToServerComponentService, FormatImageClientToServerComponentService>();
builder.Services.AddScoped<INewsBlogClientToServerComponentService, NewsBlogClientToServerComponentService>();
builder.Services.AddScoped<IRecommendationItemClientToServerComponentService, RecommendationItemClientToServerComponentService>();
builder.Services.AddScoped<IRecommendationListClientToServerComponentService, RecommendationListClientToServerComponentService>();
builder.Services.AddScoped<IStoreClientToServerComponentService, StoreClientToServerComponentService>();
builder.Services.AddScoped<IStoreWASMComponentService, StoreWASMComponentService>();
builder.Services.AddScoped<IUserClientToServerComponentService, UserClientToServerComponentService>();

builder.Services.AddScoped<IGoogleCaptchaClientToServerService, GoogleCaptchaClientToServerService>();
builder.Services.AddScoped<IEmailClientToServerComponentService, EmailClientToServerComponentService>();

// Audit Service
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

/* IOptions for UrlWhitelistOptions */
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<UrlWhitelistOptions>>().Value);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ModeratorsOnly", policy => policy.RequireRole("Moderator"));
    options.AddPolicy("AlphaUsersOnly", policy => policy.RequireRole("AlphaUser"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

var modelBuilder = new ODataConventionModelBuilder();
// Use singular entity set name to match the OData controller name `ProductController` so
// the endpoint will be reachable at `odata/Product`.
modelBuilder.EntitySet<Product>("Product");
modelBuilder.EntitySet<Format>("Format");
modelBuilder.EntitySet<FormatRevision>("FormatRevision");

var edmModel = modelBuilder.GetEdmModel();

// Register controllers and OData with the built EDM model so routes like
// 'odata/Product' are available.
builder.Services.AddControllers().AddOData(options =>
    options.AddRouteComponents("odata", edmModel)
           .Select()
           .Filter()
           .OrderBy()
           .Expand()
           .Count()
           .SetMaxTop(10));

builder.Services.Configure<JsonOptions>(options => {
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAzureClients(azureBuilder =>
{
    azureBuilder.AddBlobServiceClient(builder.Configuration["storageConnection"]);
});

/* Logging */
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddAutoComplete();
builder.Services.AddSortable();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddRadzenComponents();
builder.Services.AddGoogleCaptcha(configuration => {
    configuration.V2SiteKey = "";
    configuration.DefaultVersion = CaptchaConfiguration.Version.V2;
    configuration.DefaultTheme = CaptchaConfiguration.Theme.Light;
    configuration.DefaultLanguage = CaptchaLanguages.English;
});

var app = builder.Build();

TypeAdapterConfig<Availability, AvailabilityResponse>
    .NewConfig()
    .Map(dest => dest.URL, src => src.URL);

TypeAdapterConfig<Category, CategoryResponse>
    .NewConfig()
    .Map(dest => dest.lockedReason, src => src.lockedReason)
    .Map(dest => dest.CreatorsTitle, src => src.CreatorsTitle)
    .Map(dest => dest.FormatsTitle, src => src.FormatsTitle)
    .Map(dest => dest.TypeTitle, src => src.TypeTitle)
    .Map(dest => dest.EditionTitle, src => src.EditionTitle);

TypeAdapterConfig<Product, ProductResponse>
    .NewConfig()
    .Map(dest => dest.AIAssistsSource, src => src.AIAssistsSource)
    .Map(dest => dest.lockedReason, src => src.lockedReason);

TypeAdapterConfig<Format, FormatResponse>
    .NewConfig()
    .Map(dest => dest.Product, src => src.Product);

TypeAdapterConfig<FormatType, FormatTypeResponse>
    .NewConfig()
    .Map(dest => dest.ImageUrl, src => src.ImageUrl);

TypeAdapterConfig<ApplicationUser, UserResponse>
    .NewConfig()
    .Map(dest => dest.DateOfBirth, src => src.DateOfBirth);

TypeAdapterConfig<NewsBlog, NewsBlogResponse>
    .NewConfig()
    .Map(dest => dest.CoverImageUrl, src => src.CoverImageUrl)
    .Map(dest => dest.StickToFrontPage, src => src.StickToFrontPage);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var logger = services.GetRequiredService<ILogger<ApplicationDbContextSeed>>();

        await ApplicationDbContextSeed.SeedAsync(context, userManager, roleManager, logger);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured while seeding the database");
    }
}

// Middleware to handle exceptions globally
app.UseMiddleware<ErrorHanderMiddleware>();

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

// Add support for serving Blazor WebAssembly files
app.MapStaticAssets();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); // for API Calls

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GoMontag_WebApp.Client._Imports).Assembly);

app.Run();
