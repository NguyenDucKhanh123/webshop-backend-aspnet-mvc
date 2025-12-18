using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopApp.Data;
using WebShopApp.Models;
using WebShopApp.Service;

namespace WebShopApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CartController(CartService cartService, UserManager<ApplicationUser> userManager,ApplicationDbContext context)
        {
            _cartService = cartService;
            _userManager = userManager;
            _context = context;
        }

        private async Task<string> GetUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user.Id;
        }

        public async Task<IActionResult> Index()
        {
            var userId = await GetUserIdAsync();
            var items = await _cartService.GetCartItemsAsync(userId);
            return View(items);
        }

        public async Task<IActionResult> Add(int productId)
        {
            var userId = await GetUserIdAsync();
            await _cartService.AddToCartAsync(userId, productId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int productId)
        {
            var userId = await GetUserIdAsync();
            await _cartService.RemoveFromCartAsync(userId, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = _userManager.GetUserId(User);

            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                TempData["SwalMessage"] = "Your cart is empty!";
                TempData["SwalType"] = "warning";
                return RedirectToAction("Index");
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                OrderItems = new List<OrderItem>(),
                TotalAmount = 0
            };

            foreach (var item in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                };

                order.TotalAmount += orderItem.Quantity * orderItem.Price;
                order.OrderItems.Add(orderItem);
            }

            _context.Orders.Add(order);

            // Xóa cart items sau khi đặt hàng
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();

            TempData["SwalMessage"] = "Order placed successfully!";
            TempData["SwalType"] = "success";

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> OrderHistory()
        {
            var userId = _userManager.GetUserId(User);

            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

    }

}
