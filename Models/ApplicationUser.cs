using Microsoft.AspNetCore.Identity;

namespace WebShopApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        // Navigation properties
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
