using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopApp.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }


}
