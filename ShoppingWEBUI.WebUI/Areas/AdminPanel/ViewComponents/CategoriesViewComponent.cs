using Microsoft.AspNetCore.Mvc;
using ShoppingWEBUI.Core.DTO.Category;

namespace ShoppingWEBUI.WebUI.Areas.AdminPanel.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult>InvokeAsync(List<CategoryDTO> categories)
        {
            return View("~/Areas/AdminPanel/Views/Shared/Component/Category/Categories.cshtml", categories);
        }
    }
}
