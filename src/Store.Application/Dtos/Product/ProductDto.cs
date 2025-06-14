using Microsoft.AspNetCore.Mvc;
using Store.Application.Dtos.Category;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Store.Application.Dtos.Product;

/// <summary>
/// Product data transfer object for API responses
/// </summary>
[Produces("application/json")]
public class ProductDto
{
    /// <summary>
    /// Unique identifier for the product
    /// </summary>
    /// <example>123</example>
    [Required]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Name of the product
    /// </summary>
    /// <example>Mountain-100 Silver, 38</example>
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Manufacturer's product number
    /// </summary>
    /// <example>BK-M101-S38</example>
    [Required]
    [StringLength(25)]
    [JsonPropertyName("productNumber")]
    public string ProductNumber { get; set; }

    /// <summary>
    /// Selling price in USD
    /// </summary>
    /// <example>1000.00</example>
    [Required]
    [Range(0.01, 100000)]
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Product size (if applicable)
    /// </summary>
    /// <example>38</example>
    [StringLength(5)]
    [JsonPropertyName("size")]
    public string? Size { get; set; }

    /// <summary>
    /// Product color
    /// </summary>
    /// <example>Silver</example>
    [StringLength(30)]
    [JsonPropertyName("color")]
    public string? Color { get; set; }

    /// <summary>
    /// Weight in pounds
    /// </summary>
    /// <example>20.5</example>
    [Range(0, 1000)]
    [JsonPropertyName("weight")]
    public decimal? Weight { get; set; }

    /// <summary>
    /// Date when product became available
    /// </summary>
    /// <example>2023-01-01</example>
    [Required]
    [JsonPropertyName("sellStartDate")]
    public DateTime SellStartDate { get; set; }

    /// <summary>
    /// Date when product was discontinued (if applicable)
    /// </summary>
    /// <example>2024-01-01</example>
    [JsonPropertyName("discontinuedDate")]
    public DateTime? DiscontinuedDate { get; set; }

    public SubCategoryDto? SubCategory { get; set; }

    public string SubCategoryName { get; set; }
}