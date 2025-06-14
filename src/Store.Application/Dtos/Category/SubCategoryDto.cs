using Store.Application.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Dtos.Category
{
    public class SubCategoryDto
    {
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<ProductDto>? Products { get; set; }
    }
}
