using Core.Entities;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.SeedData;
using Microsoft.AspNetCore.Identity;
using vimmvc.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddInfrastructureConfiguration(builder.Configuration);

var app = builder.Build();

//seeding section
//var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
//using (var scope = scopeFactory.CreateScope())
//{
    //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    //var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //context.Database.MigrateAsync().Wait();
    //DbInitializer.SeedRoleAsync(context, roleManager).Wait();
    //DbInitializer.SeedUserAsync(context, userManager).Wait();
//}

//seeding section ends

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
string? port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    app.Urls.Add("http://*:" + port);
}
app.Run();
