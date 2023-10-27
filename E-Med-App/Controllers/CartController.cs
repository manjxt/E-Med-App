using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using E_Med_App.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Med_App.Data;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

[Route("[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
    {
        return await _context.Carts.Include(c => c.Items).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Cart>> CreateCart(Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCarts), new { id = cart.Id }, cart);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(int id)
    {
        var cart = await _context.Carts.FindAsync(id);

        if (cart == null)
        {
            return NotFound();
        }

        // Remove the cart
        _context.Carts.Remove(cart);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{cartId}/items")]
    public async Task<ActionResult<CartItem>> AddItemToCart(int cartId, Medicine item)
    {
        var cart = await _context.Carts.FindAsync(cartId);

        if (cart == null)
        {
            return NotFound();
        }

        /*cart.Items.Add(item);*/
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCarts", new { id = cart.Id }, item);
    }

    [HttpDelete("{cartId}/items/{itemId}")]
    public async Task<ActionResult<CartItem>> RemoveItemFromCart(int cartId, int itemId)
    {
        var cart = await _context.Carts.FindAsync(cartId);

        if (cart == null)
        {
            return NotFound();
        }

        var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

        if (item == null)
        {
            return NotFound();
        }

        cart.Items.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}


