using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Client.Installer
{
    public class InstallerConfigureService : IInstaller
    {
        public void Installer(IServiceCollection servieCollection, IConfiguration configuration)
        {
            servieCollection.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            // Add services to the container.
            servieCollection.AddControllersWithViews();
            servieCollection.AddLocalization(options => options.ResourcesPath = "Resources");
            servieCollection.AddControllersWithViews()
                .AddViewLocalization
                    (LanguageViewLocationExpanderFormat.SubFolder)
                .AddDataAnnotationsLocalization();
            servieCollection.AddDistributedMemoryCache();
            servieCollection.AddSession(options => { options.IdleTimeout = TimeSpan.FromHours(1); });//You can set Time
            servieCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            servieCollection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(o =>
               {
                   o.LoginPath = "/Home/LoginPage";
               });
            servieCollection.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en-US", "km", "th-TH" };
                options.SetDefaultCulture(supportedCultures[0])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });
            servieCollection.AddSignalR(hubOptions =>
            {
                hubOptions.ClientTimeoutInterval = TimeSpan.FromDays(1);
                hubOptions.KeepAliveInterval = TimeSpan.FromDays(1);
            });
            /*hubOptions =>
            {
                hubOptions.ClientTimeoutInterval = TimeSpan.FromDays(1);
                hubOptions.KeepAliveInterval = TimeSpan.FromDays(1);
            }*/
        }
    }
}
