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

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDbContext>();





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


    //options.Events.OnCreatingTicket = async context =>
    //{
    //    // Retrieve the user details from Google
    //    var user = context.Principal;
    //    var name = user?.FindFirstValue(ClaimTypes.Name);
    //    var email = user?.FindFirstValue(ClaimTypes.Email);

    //    // If the name is missing, you can set it to the email or another value
    //    if (string.IsNullOrEmpty(name))
    //    {
    //        name = email; // Use email as a fallback if 'Name' is empty
    //    }

    //    // Ensure the 'Name' claim is added to the user's claims
    //    var claimsIdentity = (ClaimsIdentity)context.Principal.Identity;
    //    claimsIdentity.AddClaim(new Claim("Name", name));

    //    // This step ensures that the 'Name' field is set when the user is created
    //    var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
    //    var existingUser = await userManager.FindByEmailAsync(email);

    //    if (existingUser != null)
    //    {
    //        // Update the existing user
    //        existingUser.Name = name; // Set the Name field
    //        await userManager.UpdateAsync(existingUser);
    //    }

    //    await Task.CompletedTask;
    //};






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
