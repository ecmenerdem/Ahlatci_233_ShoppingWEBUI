using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using ShoppingWEBUI.Core.DTO.Login;
using ShoppingWEBUI.Core.Result;
using ShoppingWEBUI.Helper.SessionHelper;

namespace ShoppingWEBUI.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("/AdminAccount/Login")]
        public IActionResult Index()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/AdminLogin")]
        public async Task<IActionResult> AdminLogin(LoginDTO loginDTO)
        {
            var url = "http://localhost:5249/Login";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(loginDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<LoginDTO>>(response.Content);

            if (responseObject.Data!=null)
            {
                //var userStringData = JsonConvert.SerializeObject(responseObject.Data);

                //_httpContextAccessor.HttpContext.Session.SetString("AdSoyad", responseObject.Data.AdSoyad);
                //_httpContextAccessor.HttpContext.Session.SetString("EPosta", responseObject.Data.EPosta);
                //_httpContextAccessor.HttpContext.Session.SetString("Adres", responseObject.Data.Adres);

                //_httpContextAccessor.HttpContext.Session.SetString("User", userStringData);

                SessionManager.LoggedUser = responseObject.Data;
                SessionManager.Token = responseObject.Data.Token;


                return RedirectToAction("Index", "Home");
            }

            //Session
            ViewData["LoginError"] = "Kullanıcı Adı Veya Şifreniz Yanlış";

            return View("Index");
        }


    }
}
