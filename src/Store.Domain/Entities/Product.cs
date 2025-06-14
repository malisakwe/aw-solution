using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Domain.Entities;

public class Product :BaseEntity
{

    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ProductNumber { get; set; } = string.Empty;
    public decimal ListPrice { get; set; }

    public int? SubCategoryId { get; set; }

    public SubCategory SubCategory { get; set; } = null!;


}