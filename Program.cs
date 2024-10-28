using ManoTourism.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;
using NToastNotify;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using DevExpress.XtraCharts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
#region Identity Configuration

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(45);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Admin/AccessDenied";
    options.SlidingExpiration = true;
});

#endregion
#region "Data Context"

builder.Services.AddDbContext<ManoContext>(options => options.UseSqlServer(connectionString));

#endregion

#region "Localization"

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddRazorPages().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null).AddDataAnnotationsLocalization(
               options =>
               {
                   var type = typeof(ManoTourism.SharedResource);
                   var assembblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
                   var factory = builder.Services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                   var localizer = factory.Create("SharedResource", assembblyName.Name);
                   options.DataAnnotationLocalizerProvider = (t, f) => localizer;
               }
               );
builder.Services.AddControllers().AddDataAnnotationsLocalization(
          options =>
          {
              var type = typeof(ManoTourism.SharedResource);
              var assembblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
              var factory = builder.Services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
              var localizer = factory.Create("SharedResource", assembblyName.Name);
              options.DataAnnotationLocalizerProvider = (t, f) => localizer;
          });
builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization(
          options =>
          {
              var type = typeof(ManoTourism.SharedResource);
              var assembblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
              var factory = builder.Services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
              var localizer = factory.Create("SharedResource", assembblyName.Name);
              options.DataAnnotationLocalizerProvider = (t, f) => localizer;
          }
          );
builder.Services.AddDevExpressControls();
builder.Services.AddMvc();
builder.Services.ConfigureReportingServices(configurator =>
{
    configurator.ConfigureWebDocumentViewer(viewerConfigurator =>
    {
        viewerConfigurator.UseCachedReportSourceBuilder();
    });
});
#endregion
builder.Services.AddRazorPages();
builder.Services.AddRazorPages().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = true,
    PreventDuplicates = true,
    CloseButton = true
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
var app = builder.Build();
app.UseDevExpressControls();
System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();


}
else
{
    app.UseExceptionHandler("/Error");
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
var supportedCultures = new[]
           {
                new CultureInfo("en-US"),

                new CultureInfo("ar-EG")

            };


app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});
app.UseEndpoints(endpoints =>
{

    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.Run();
