using INTEXAPP2.Data;
using INTEXAPP2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString));

builder.Services.AddDbContext<mummiesContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("Mummies"));
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
{


        //Password requirements
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequiredLength = 15;

        //Rate Limiting (Throttling) See NIST SP800-63b 5.2.2 https://pages.nist.gov/800-63-3/sp800-63b.html#throttle
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

        

    })
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();


builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential 
    // cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;

    options.MinimumSameSitePolicy = SameSiteMode.None;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.Use(async (context, next) => {
    context.Response.Headers.Add("Content-Security-Policy-Report-Only", "default-src 'self';" +
        "script-src 'self' 'sha256-m1igTNlg9PL5o60ru2HIIK6OPQet2z9UgiEAhCyg/RU=' 'sha256-m1igTNlg9PL5o60ru2HIIK6OPQet2z9UgiEAhCyg/RU='" +
        "'unsafe-eval'; " +
        "style-src 'self' " +
        "'sha256-47DEQpj8HBSa+/TImW+5JCeuQeRkm5NMpJWZG3hSuFU=' " +
        "'sha256-tVFibyLEbUGj+pO/ZSi96c01jJCvzWilvI5Th+wLeGE=' 'unsage-eval'" +
        "font-src 'self'; " +
        "img-src 'self'; " +
        "connect-src 'self'");

    await next();
});



app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute("Second", "{controller}/{action}/{id?}");
app.MapControllerRoute("Paging", "{controller=Home}/{action=BurialSummary}/{pageNum}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();
