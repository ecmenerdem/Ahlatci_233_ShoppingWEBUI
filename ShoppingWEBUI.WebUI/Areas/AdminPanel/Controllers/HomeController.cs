using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingWEBUI.Core.DTO.Login;
using ShoppingWEBUI.Core.Model;
using ShoppingWEBUI.Core.Result;
using ShoppingWEBUI.Helper.SessionHelper;

namespace ShoppingWEBUI.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HttpGet("/Admin/Anasayfa")]
        public IActionResult Index()
        {
            
            //var user = JsonConvert.DeserializeObject<LoginDTO>(_contextAccessor.HttpContext.Session.GetString("User"));

            UserViewModel userViewModel = new()
            {
                AdSoyad = SessionManager.LoggedUser.AdSoyad,
                EPosta = SessionManager.LoggedUser.EPosta,
                Adres = SessionManager.LoggedUser.Adres
            };

            return View(userViewModel);
        }
    }
}
