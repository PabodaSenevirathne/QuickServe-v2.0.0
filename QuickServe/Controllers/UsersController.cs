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
    public class UsersController : ControllerBase
    {
        private readonly QuickServeContext _context;

        public UsersController(QuickServeContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return user;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Users>> AddUser(Users user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, Users user)
        {
            if (id != user.Id)
            {
                return BadRequest("Invalid user ID.");
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound("User not found.");
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "User updated successfully." });
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User deleted successfully." });
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
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
//    public class UsersController : ControllerBase
//    {
//        private readonly QuickServeContext _context;

//        public UsersController(QuickServeContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Users
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
//        {
//          if (_context.Users == null)
//          {
//              return NotFound();
//          }
//            return await _context.Users.ToListAsync();
//        }

//        // GET: api/Users/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Users>> GetUsers(int id)
//        {
//          if (_context.Users == null)
//          {
//              return NotFound();
//          }
//            var users = await _context.Users.FindAsync(id);

//            if (users == null)
//            {
//                return NotFound();
//            }

//            return users;
//        }

//        // PUT: api/Users/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutUsers(int id, Users users)
//        {
//            if (id != users.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(users).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!UsersExists(id))
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

//        // POST: api/Users
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Users>> PostUsers(Users users)
//        {
//          if (_context.Users == null)
//          {
//              return Problem("Entity set 'QuickServeContext.Users'  is null.");
//          }
//            _context.Users.Add(users);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
//        }

//        // DELETE: api/Users/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteUsers(int id)
//        {
//            if (_context.Users == null)
//            {
//                return NotFound();
//            }
//            var users = await _context.Users.FindAsync(id);
//            if (users == null)
//            {
//                return NotFound();
//            }

//            _context.Users.Remove(users);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool UsersExists(int id)
//        {
//            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
