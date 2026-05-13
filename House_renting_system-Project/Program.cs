using House_renting_system_Project.Data.Data;
using House_renting_system_Project.Data.Data.Entities;
using House_renting_system_Project.Extensions;
using House_renting_system_Project.Middlewares;
using House_Renting_System_Project.Services.Contracts;
using House_Renting_System_Project.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection");

builder
    .Services
    .AddDbContext<HouseRentingDbContext>(
        options => options.UseSqlServer(connectionString))
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<HouseRentingDbContext>()
    .AddDefaultTokenProviders();

builder
    .Services
    .ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Home/Error?statusCode=401";
    });

builder
    .Services
    .AddSingleton<IStatisticsService, StatisticsService>()
    .AddScoped<IHouseService, HouseService>()
    .AddScoped<IAuthService, AuthService>()
    .AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<HouseRentingDbContext>();
    await db.Database.MigrateAsync();
}

await app.SeedHouses();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/ServerError");
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseStatistics();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app
    .MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

await app.RunAsync(app.Lifetime.ApplicationStopping);
