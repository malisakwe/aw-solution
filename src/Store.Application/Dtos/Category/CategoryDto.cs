namespace Store.Application.Dtos.Category
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }
        
        public ICollection<SubCategoryDto>? SubCategories { get; set; }
    }
}
