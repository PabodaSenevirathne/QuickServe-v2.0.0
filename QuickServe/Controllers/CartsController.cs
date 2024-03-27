using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickServe.Data;
using QuickServe.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickServe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly QuickServeContext _context;

        public CartsController(QuickServeContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCart()
        {
            var carts = await _context.Cart.ToListAsync();
            if (carts == null || !carts.Any())
            {
                return NotFound("Cart is empty.");
            }
            return carts;
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCartByCartId(int id)
        {
            var cartItem = await _context.Cart.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }
            return cartItem;
        }

        // POST: api/Carts
        [HttpPost]
        public async Task<ActionResult<Cart>> AddToCart(Cart cartItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cart.Add(cartItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCartByCartId), new { id = cartItem.CartId }, cartItem);
        }

        // PUT: api/Carts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, Cart cartItem)
        {
            if (id != cartItem.CartId)
            {
                return BadRequest("Invalid cart ID.");
            }

            _context.Entry(cartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartItemExists(id))
                {
                    return NotFound("Cart item not found.");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Cart item updated successfully.");
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cartItem = await _context.Cart.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            _context.Cart.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok("Cart item deleted successfully.");
        }

        // GET: api/Carts/User/5
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartByUserId(int userId)
        {
            var cartItems = await _context.Cart.Where(c => c.UserId == userId).ToListAsync();
            if (cartItems == null || !cartItems.Any())
            {
                return NotFound("Cart is empty for the specified user ID.");
            }
            return cartItems;
        }

        private bool CartItemExists(int id)
        {
            return _context.Cart.Any(e => e.CartId == id);
        }
    }
}
