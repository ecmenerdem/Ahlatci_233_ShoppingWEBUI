using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWEBUI.Core.DTO.Category
{
    public class CategoryDTO
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }

    }
}
