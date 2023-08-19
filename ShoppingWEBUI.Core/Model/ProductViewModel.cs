using ShoppingWEBUI.Core.DTO.Category;
using ShoppingWEBUI.Core.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWEBUI.Core.Model
{
    public class ProductViewModel
    {
        public List<ProductDTO> Products { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}
