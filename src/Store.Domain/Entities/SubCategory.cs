using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class SubCategory: BaseEntity
    {
        public int SubCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
