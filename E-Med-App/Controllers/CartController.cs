using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using E_Med_App.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Med_App.Data;
using System.Linq;

/*
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
*/


namespace MedEcommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;



        public CartsController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            return await _context.Carts.ToListAsync();
        }



        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCart(int id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var cart = await _context.Carts.Include(m => m.Medicine).Where(c => c.UserId == id).ToListAsync();



            if (cart == null)
            {
                return NotFound();
            }



            return cart;
        }



        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Cart>>> PutCart(int id, Cart cart)
        {
            if (id != cart.Id)
            {
                return BadRequest();
            }



            _context.Entry(cart).State = EntityState.Modified;



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }



            var item = _context.Carts.Include(u => u.Medicine).Where(u => u.Id == id).ToList();
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }



        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            if (_context.Carts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Carts'  is null.");
            }
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();



            var items = _context.Carts.Include(u => u.Medicine).ToList();



            return CreatedAtAction("GetCart", new { id = cart.Id }, items);
        }



        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }



            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();



            return NoContent();
        }



        private bool CartExists(int id)
        {
            return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}