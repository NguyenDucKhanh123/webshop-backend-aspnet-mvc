using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShopApp.Data;
using WebShopApp.Models;

namespace WebShopApp.Service
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Cart> GetOrCreateCartAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedDate = DateTime.Now
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity = 1)
        {
            var cart = await GetOrCreateCartAsync(userId);

            // Đảm bảo CartItems đã được load và khởi tạo
            if (cart.CartItems == null)
            {
                // Load lại cart với CartItems nếu chưa Include trước đó
                cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.Id == cart.Id);

                // Nếu vẫn null thì khởi tạo mới
                if (cart.CartItems == null)
                    cart.CartItems = new List<CartItem>();
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            await _context.SaveChangesAsync();
        }


        public async Task RemoveFromCartAsync(string userId, int productId)
        {
            var cart = await GetOrCreateCartAsync(userId);
            var item = cart.CartItems.FirstOrDefault(i => i.Id == productId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await GetOrCreateCartAsync(userId);
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetCartItemsAsync(string userId)
        {
            var cart = await GetOrCreateCartAsync(userId);
            return cart.CartItems.ToList();
        }

    }
}
