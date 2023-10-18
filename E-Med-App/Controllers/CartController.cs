using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        [Route("AddCart")]
        public async Task<IActionResult> AddCart(Cart cart)
        {
            try
            {
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();

                return Ok("Cart added successfully.");
            }
            catch (DbUpdateException)
            {
                return BadRequest("Failed to add the cart.");
            }
        }

        [HttpGet]
        [Route("GetCarts")]
        public async Task<IActionResult> GetCarts()
        {
            var carts = await _context.Carts.ToListAsync();
            return Ok(carts);
        }

        [HttpGet]
        [Route("GetCart/{id}")]
        public async Task<IActionResult> GetCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            return Ok(cart);
        }

        [HttpPut]
        [Route("EditCart")]
        public async Task<IActionResult> EditCart(Cart cart)
        {
            try
            {
                _context.Carts.Update(cart);
                await _context.SaveChangesAsync();

                return Ok("Cart updated successfully.");
            }
            catch (DbUpdateException)
            {
                return BadRequest("Failed to update the cart.");
            }
        }

        [HttpDelete]
        [Route("DeleteCart/{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return Ok("Cart deleted successfully.");
        }

        [HttpPost]
        [Route("AddCartItem")]
        public async Task<IActionResult> AddCartItem(CartItem cartItem)
        {
            try
            {
                _context.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();

                return Ok("Cart item added successfully.");
            }
            catch (DbUpdateException)
            {
                return BadRequest("Failed to add the cart item.");
            }
        }

        [HttpGet]
        [Route("GetCartItems")]
        public async Task<IActionResult> GetCartItems()
        {
            var cartItems = await _context.CartItems.ToListAsync();
            return Ok(cartItems);
        }

        [HttpGet]
        [Route("GetCartItem/{id}")]
        public async Task<IActionResult> GetCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            return Ok(cartItem);
        }

        [HttpPut]
        [Route("EditCartItem")]
        public async Task<IActionResult> EditCartItem(CartItem cartItem)
        {
            try
            {
                _context.CartItems.Update(cartItem);
                await _context.SaveChangesAsync();

                return Ok("Cart item updated successfully.");
            }
            catch (DbUpdateException)
            {
                return BadRequest("Failed to update the cart item.");
            }
        }

        [HttpDelete]
        [Route("DeleteCartItem/{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok("Cart item deleted successfully.");
        }
    }
}



/*
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
*/
