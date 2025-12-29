using Acxess.Identity;
using Acxess.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddIdentityModule(builder.Configuration);

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
