using Store.Application.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Store.Application.Dtos.Product
{

    /// <summary>
    /// Product data transfer object for creation operations
    /// </summary>
    public class CreateProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(25)]
        public string ProductNumber { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 100000)]
        public decimal Price { get; set; }

        [StringLength(5)]
        public string? Size { get; set; }

        [StringLength(30)]
        public string? Color { get; set; }

        [Range(0, 1000)]
        public decimal? Weight { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Sell start date must be in the future")]
        public DateTime SellStartDate { get; set; }
    }

}
