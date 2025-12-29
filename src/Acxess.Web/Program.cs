using Acxess.Catalog;
using Acxess.Identity;
using Acxess.Infrastructure.Extensions;
using Acxess.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
        .AddProblemDetails();

builder.Services.AddRazorPages();

builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddCatalogModule(builder.Configuration);

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
