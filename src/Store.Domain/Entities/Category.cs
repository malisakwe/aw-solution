using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Category: BaseEntity
    {

        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();

    }
}
