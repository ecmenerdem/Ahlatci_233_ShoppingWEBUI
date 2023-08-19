using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using ShoppingWEBUI.Core.DTO.Category;
using ShoppingWEBUI.Core.DTO.Login;
using ShoppingWEBUI.Core.Result;
using ShoppingWEBUI.Helper.SessionHelper;
using System.Net;

namespace ShoppingWEBUI.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        [HttpGet("/Admin/Kategori")]
        public async Task<ActionResult> Index()
        {
            var url = "http://localhost:5249/ActiveCategories";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<CategoryDTO>>>(response.Content);
            var categoryList = responseObject.Data;
            return View(categoryList.ToList());
        }


        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryDTO category)
        {
            var url = "http://localhost:5249/AddCategory";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            var body = JsonConvert.SerializeObject(category);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<CategoryDTO>>(response.Content);

            if (response.StatusCode==HttpStatusCode.OK)
            {
                return Json(new { success = true, message = responseObject.Mesaj, data = responseObject.Data });
            }
            else if (response.StatusCode==HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, message = responseObject.Mesaj, data = responseObject.HataBilgisi });
            }

            else
            {
                return Json(new { success = false, message = "Hata Oluştu" });
            }

        }


        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDTO category)
        {
            var url = "http://localhost:5249/UpdateCategory";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            var body = JsonConvert.SerializeObject(category);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, message = responseObject.Mesaj, data = responseObject.Data });
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, message = responseObject.Mesaj, data = responseObject.HataBilgisi });
            }

            else
            {
                return Json(new { success = false, message = "Hata Oluştu" });
            }

        }


        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/RemoveCategory")]
        public async Task<IActionResult> RemoveCategory(CategoryDTO categoryDTO)
        {

            var url = "http://localhost:5249/RemoveCategory/";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            var body = JsonConvert.SerializeObject(categoryDTO);
            request.AddBody(body, "application/json");
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<bool>>(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, message = responseObject.Mesaj, data = responseObject.Data });
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, message = responseObject.Mesaj, data = responseObject.HataBilgisi });
            }

            else
            {
                return Json(new { success = false, message = "Hata Oluştu" });
            }

        }


        [HttpGet("/Admin/Kategori/{categoryGUID}")]
        public async Task<ActionResult> GetCategoryDetail(Guid categoryGUID)
        {
            var url = "http://localhost:5249/Category/"+ categoryGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<CategoryDTO>>(response.Content);

            return Json(new { success = true, data = responseObject.Data});
        }
    }
}
