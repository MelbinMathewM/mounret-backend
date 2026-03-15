namespace Mounret.API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public string? Dimensions { get; set; }

        public string? Material { get; set; }

        public string? AdditionalInfo { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}