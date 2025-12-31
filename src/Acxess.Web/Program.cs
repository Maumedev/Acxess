using Acxess.Catalog;
using Acxess.Identity;
using Acxess.Infrastructure.Extensions;
using Acxess.Infrastructure.Middlewares;
using Acxess.Membership;
using Acxess.Billing;
using Acxess.Marketing;
using Acxess.Web.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
        .AddProblemDetails();

builder.Services.AddScoped<PageExceptionFilter>();

builder.Services.AddRazorPages()
    .AddMvcOptions(options =>
    {
        options.Filters.Add<PageExceptionFilter>();
    });;

builder.Services.AddAcxessInfrastructure(); 

builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddMembershipModule(builder.Configuration);
builder.Services.AddBillingModule(builder.Configuration);
builder.Services.AddMarketingModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("RUN_MIGRATIONS") == "true")
{
    await app.ApplyMigrationsAndSeedsAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
