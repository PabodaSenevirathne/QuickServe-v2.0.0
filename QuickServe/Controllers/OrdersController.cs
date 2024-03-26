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
    public class OrdersController : ControllerBase
    {
        private readonly QuickServeContext _context;

        public OrdersController(QuickServeContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            if (orders == null || !orders.Any())
            {
                return NotFound();
            }
            return orders;
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Orders>> AddOrder(Orders order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        // GET: api/Orders/User/5
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrdersByUserId(int userId)
        {
            var orders = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
            if (orders == null || !orders.Any())
            {
                return NotFound();
            }
            return orders;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}














//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using QuickServe.Data;
//using QuickServe.Models;

//namespace QuickServe.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrdersController : ControllerBase
//    {
//        private readonly QuickServeContext _context;

//        public OrdersController(QuickServeContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Orders
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Orders>>> GetOrders()
//        {
//          if (_context.Orders == null)
//          {
//              return NotFound();
//          }
//            return await _context.Orders.ToListAsync();
//        }

//        // GET: api/Orders/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Orders>> GetOrders(int id)
//        {
//          if (_context.Orders == null)
//          {
//              return NotFound();
//          }
//            var orders = await _context.Orders.FindAsync(id);

//            if (orders == null)
//            {
//                return NotFound();
//            }

//            return orders;
//        }

//        // PUT: api/Orders/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutOrders(int id, Orders orders)
//        {
//            if (id != orders.OrderId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(orders).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!OrdersExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Orders
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Orders>> PostOrders(Orders orders)
//        {
//          if (_context.Orders == null)
//          {
//              return Problem("Entity set 'QuickServeContext.Orders'  is null.");
//          }
//            _context.Orders.Add(orders);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetOrders", new { id = orders.OrderId }, orders);
//        }

//        // DELETE: api/Orders/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteOrders(int id)
//        {
//            if (_context.Orders == null)
//            {
//                return NotFound();
//            }
//            var orders = await _context.Orders.FindAsync(id);
//            if (orders == null)
//            {
//                return NotFound();
//            }

//            _context.Orders.Remove(orders);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool OrdersExists(int id)
//        {
//            return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
//        }
//    }
//}
