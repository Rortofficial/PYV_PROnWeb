using Client.Installer;
using Client.SignalR;
//using Client.Connection;
//using Client.Repository;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.Razor;
//using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var installer = typeof(Program).Assembly.ExportedTypes.Where(x => typeof(IInstaller).IsAssignableFrom(x) &&
        !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
installer.ForEach(installer => installer.Installer(builder.Services, configuration));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
//app.UseHttpsRedirection();
app.UseStaticFiles();
var supportedCultures = new[] { "en-US", "km", "th-TH" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);
app.UseRouting();
//app.UseHttpContext();
IWebHostEnvironment env = app.Environment;
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=LoginPage}/{id?}"); //LoginPage
app.MapHub<ChatHub>("/chatHub");
    //UserDefinedLayoutAndReportManager._RegisterJobSheetTruckingLayout("JOBSHEETRUCKING", "JOBSHEETRUCKING", "JOBSHEETRUCKING", "JOBSHEETRUCKING", "JOBSHEETRUCKING", "tes.rpt", SAP_Driver_oCompany._CheckingStatusOCompany(), env);
    //string TypeName, string AddonName, string AddonFormType, string MenuID, string Name, string RepName, Company oCompany
app.Run();
