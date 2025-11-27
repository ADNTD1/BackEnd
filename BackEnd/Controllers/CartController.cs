using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;
using Ecomerce_Back_End.Data;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }
        public class AddToCartRequest
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        // GET: api/cart/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<Cart>> GetCart(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                    .ThenInclude(i => i.Product)  // <-- CORRECTED
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // POST: api/cart/{userId}/add
        // Agrega esta clase al inicio del archivo CartController.cs
        // Luego cambia el método AddItem:
        [HttpPost("{userId}/add")]
        public async Task<ActionResult> AddItem(int userId, [FromBody] AddToCartRequest request)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                });
            }

            await _context.SaveChangesAsync();
            return Ok(cart);
        }

        // PUT: api/cart/update/{itemId}
        [HttpPut("update/{itemId}")]
        public async Task<ActionResult> UpdateItem(int itemId, [FromBody] CartItem updatedItem)
        {
            var item = await _context.CartItems.FindAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            item.Quantity = updatedItem.Quantity;
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        // DELETE: api/cart/delete/{itemId}
        [HttpDelete("delete/{itemId}")]
        public async Task<ActionResult> DeleteItem(int itemId)
        {
            var item = await _context.CartItems.FindAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
