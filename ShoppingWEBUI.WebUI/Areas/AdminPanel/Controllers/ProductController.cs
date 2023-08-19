using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using ShoppingWEBUI.Core.DTO.Category;
using ShoppingWEBUI.Core.DTO.Product;
using ShoppingWEBUI.Core.Model;
using ShoppingWEBUI.Core.Result;
using ShoppingWEBUI.Helper.SessionHelper;
using System.Net;

namespace ShoppingWEBUI.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("/Admin/Urunler")]
        public async Task<ActionResult> Index()
        {
            ProductViewModel productViewModel = new()
            {
                Products = await GetProducts(),
                Categories = await GetCategories()
            };
            return View(productViewModel);
        }


        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/UrunEkle")]
        public async Task<IActionResult>AddProduct(ProductDTO productDTO, IFormFile productImage)
        {
            if (productImage!=null)
            {
                string filename = productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 2] + "_" + Guid.NewGuid() + "." + productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 1];

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUploads", filename);

                using (var fileStream = new FileStream(uploadFolder,FileMode.Create))
                {
                    productImage.CopyTo(fileStream);
                }

                productDTO.FeaturedImage = filename;
            }

            var url = "http://localhost:5249/AddProduct";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            var body = JsonConvert.SerializeObject(productDTO);
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
        [HttpPost("/Admin/UrunGuncelle")]
        public async Task<IActionResult>UpdateProduct(ProductDTO productDTO, IFormFile productImage)
        {
            if (productImage != null)
            {
                string filename = productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 2] + "_" + Guid.NewGuid() + "." + productImage.FileName.Split('.')[productImage.FileName.Split('.').Length - 1];

                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "MediaUploads", filename);

                using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                {
                    productImage.CopyTo(fileStream);
                }

                productDTO.FeaturedImage = filename;
            }
            else
            {
                productDTO.FeaturedImage = null;
            }

            var url = "http://localhost:5249/UpdateProduct";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            var body = JsonConvert.SerializeObject(productDTO);
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


        [HttpGet("/Admin/Urun/{productGUID}")]
        public async Task<ActionResult> GetCategoryDetail(Guid productGUID)
        {
            var url = "http://localhost:5249/Product/" + productGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<ProductDTO>>(response.Content);

            return Json(new { success = true, data = responseObject.Data });
        }


        [ValidateAntiForgeryToken]
        [HttpPost("/Admin/RemoveProduct")]
        public async Task<IActionResult> RemoveProduct(Guid productGUID)
        {
            ProductDTO productDTO = new()
            {
                Guid = productGUID
            };

            var url = "http://localhost:5249/RemoveProduct/";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            var body = JsonConvert.SerializeObject(productDTO);
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



        public async Task<List<ProductDTO>> GetProducts()
        {
            var url = "http://localhost:5249/Products";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<ProductDTO>>>(response.Content);
            var productList = responseObject.Data;

            return productList;
        }
        public async Task<List<CategoryDTO>> GetCategories()
        {
            var url = "http://localhost:5249/ActiveCategories";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.Token);
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<CategoryDTO>>>(response.Content);
            var categoryList = responseObject.Data;

            return categoryList;
        }
    }
}
