using System.ComponentModel.DataAnnotations;

namespace WebShopApp.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(200)]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public bool IsNew { get; set; }
        public bool IsPromotion { get; set; }

        public IFormFile ImageFile { get; set; }
    }

}
