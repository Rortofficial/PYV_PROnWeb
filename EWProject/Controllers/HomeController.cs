//using Microsoft.AspNetCore.Authentication;
//using System.Security.Claims;
using Client.Connection;
using EWProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

namespace EWProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            if (Request.Cookies["ID"] != null)
            {
                ViewBag.UrlBase = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                return View();
            }
            else
            {
                return RedirectToAction("LoginPage", "Home");
            }
        }
        [AllowAnonymous]
        public IActionResult LoginPage()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("LoginPage", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        //[HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(string txtUserName, string txtPassword)
        {
            try
            {
                foreach (DataRow dataRow in new GetRecordByDataTable(
                             "GETUSERINFO",
                             txtUserName,
                             txtPassword,
                             "",
                             "",
                             "").GetResultDataTable().Rows)
                {
                    if (dataRow["EMPID"].ToString() == "0" || dataRow["Department"].ToString() == "")
                    {
                        return RedirectToAction("LoginPage", "Home");
                    }
                    setCookies("UserIDName", dataRow["USERIDNAME"].ToString());
                    setCookies("EmpID", dataRow["EMPID"].ToString());
                    setCookies("ID", dataRow["ID"].ToString());
                    setCookies("UserName", dataRow["UserName"].ToString());
                    setCookies("Sereis", dataRow["Sereis"].ToString());
                    setCookies("Department", dataRow["Department"].ToString());
                    setCookies("Position", dataRow["Position"].ToString());
                    setCookies("CostCenter", dataRow["CostCenter"].ToString());
                    List<string> users = new List<string>();
                    users.Add(Url.Action("Index", "Home"));
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, txtUserName),
	                    //...more claims if needed
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        //IsPersistent = login.IsPersistent,
                        ExpiresUtc = DateTime.UtcNow.AddDays(1)
                    };
                    await HttpContext.SignInAsync(principal, properties);
                    await Configure.GetAllPermission(dataRow["ID"].ToString());
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("LoginPage", "Home");
            }
            catch //(Exception ex)
            {
                return RedirectToAction("LoginPage", "Home");
            }      
        }
        #region Add Language
        public IActionResult CultureManagment(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), //culture.ToString(),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            //var cookieSetHeader = httpContextAccessor.HttpContext.Response.GetTypedHeaders().SetCookie;
            //var setCookie = Uri.UnescapeDataString(cookieSetHeader.FirstOrDefault(x => x.Name == ".AspNetCore.Culture").Value.ToString());
            return LocalRedirect(returnUrl);
        }
        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private void setCookies(string key, string values)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append(key, values, option);
        }
    }
}