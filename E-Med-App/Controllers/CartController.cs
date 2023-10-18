using Microsoft.AspNetCore.Mvc;
using E_Med_App.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Med_App.Data;
using System.Linq;

namespace E_Med_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("add-to-cart")]
        public async Task<IActionResult> AddToCart(Cart cartItem)
        {
            try
            {
                _context.Carts.Add(cartItem);
                await _context.SaveChangesAsync();

                return Ok("Item added to cart successfully.");
            }
            catch (DbUpdateException)
            {
                return BadRequest("Failed to add item to cart.");
            }
        }

        [HttpPut]
        [Route("update-cart")]
        public async Task<IActionResult> UpdateCart(Cart cartItem)
        {
            try
            {
                _context.Carts.Update(cartItem);
                await _context.SaveChangesAsync();

                return Ok("Item updated in the cart successfully.");
            }
            catch (DbUpdateException)
            {
                return BadRequest("Failed to update item in the cart.");
            }
        }

        [HttpDelete]
        [Route("delete-from-cart")]
        public async Task<IActionResult> DeleteFromCart(int cartId)
        {
            var cartItem = await _context.Carts.FindAsync(cartId);

            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok("Item deleted from cart successfully.");
        }

        [HttpGet]
        [Route("get-cart-items")]
        public async Task<IActionResult> GetCartItems(int userId)
        {
            var cartItems = await _context.Carts.Where(c => c.UserId == userId).ToListAsync();

            if (cartItems == null || cartItems.Count == 0)
            {
                return NotFound("No items found in the cart.");
            }

            return Ok(cartItems);
        }
    }
}
