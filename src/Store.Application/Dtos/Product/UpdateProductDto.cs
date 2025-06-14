using Store.Application.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Dtos.Product
{

    /// <summary>
    /// Product data transfer object for update operations
    /// </summary>
    public class UpdateProductDto
    {
        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }

        [StringLength(25)]
        public string? ProductNumber { get; set; }

        [Range(0.01, 100000)]
        public decimal? Price { get; set; }

        [StringLength(5)]
        public string? Size { get; set; }

        [StringLength(30)]
        public string? Color { get; set; }

        [Range(0, 1000)]
        public decimal? Weight { get; set; }

        [FutureDate]
        public DateTime? SellStartDate { get; set; }
    }

}
