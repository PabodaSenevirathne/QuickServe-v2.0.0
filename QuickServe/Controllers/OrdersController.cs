﻿using Microsoft.AspNetCore.Mvc;
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
                return NotFound("No orders found.");
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
                return NotFound("Order not found.");
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
                return NotFound("No orders found for the specified user ID.");
            }
            return orders;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Orders order)
        {
            if (id != order.OrderId)
            {
                return BadRequest("Invalid order ID.");
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound("Order not found.");
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Order updated successfully." });
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order canceled successfully." });
        }


        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}