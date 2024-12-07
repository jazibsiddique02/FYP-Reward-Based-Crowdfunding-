using FYP_Reward_Based_Crowdfunding_.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FYP_Reward_Based_Crowdfunding_.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;




var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount=false).AddEntityFrameworkStores<ApplicationDbContext>();





// Google Authentication code
IConfiguration configuration = builder.Configuration;

// Get the Google Auth settings
var googleAuthConfig = configuration.GetSection("Google");
string clientId = googleAuthConfig.GetValue<string>("ClientId");
string clientSecret = googleAuthConfig.GetValue<string>("ClientSecret");


builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = clientId;
    options.ClientSecret = clientSecret;

}

);




builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseAuthentication();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
