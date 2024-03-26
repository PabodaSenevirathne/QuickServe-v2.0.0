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
    public class CommentsController : ControllerBase
    {
        private readonly QuickServeContext _context;

        public CommentsController(QuickServeContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> GetComments()
        {
            var comments = await _context.Comments.ToListAsync();
            if (comments == null || !comments.Any())
            {
                return NotFound();
            }
            return comments;
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comments>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return comment;
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult<Comments>> AddComment(Comments comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comment.CommentId }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Comments/Product/5
        [HttpGet("Product/{productId}")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetCommentsByProductId(int productId)
        {
            var comments = await _context.Comments.Where(c => c.ProductId == productId).ToListAsync();
            if (comments == null || !comments.Any())
            {
                return NotFound();
            }
            return comments;
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
//    public class CommentsController : ControllerBase
//    {
//        private readonly QuickServeContext _context;

//        public CommentsController(QuickServeContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Comments
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Comments>>> GetComments()
//        {
//          if (_context.Comments == null)
//          {
//              return NotFound();
//          }
//            return await _context.Comments.ToListAsync();
//        }

//        // GET: api/Comments/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Comments>> GetComments(int id)
//        {
//          if (_context.Comments == null)
//          {
//              return NotFound();
//          }
//            var comments = await _context.Comments.FindAsync(id);

//            if (comments == null)
//            {
//                return NotFound();
//            }

//            return comments;
//        }

//        // PUT: api/Comments/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutComments(int id, Comments comments)
//        {
//            if (id != comments.CommentId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(comments).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!CommentsExists(id))
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

//        // POST: api/Comments
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Comments>> PostComments(Comments comments)
//        {
//          if (_context.Comments == null)
//          {
//              return Problem("Entity set 'QuickServeContext.Comments'  is null.");
//          }
//            _context.Comments.Add(comments);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetComments", new { id = comments.CommentId }, comments);
//        }

//        // DELETE: api/Comments/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteComments(int id)
//        {
//            if (_context.Comments == null)
//            {
//                return NotFound();
//            }
//            var comments = await _context.Comments.FindAsync(id);
//            if (comments == null)
//            {
//                return NotFound();
//            }

//            _context.Comments.Remove(comments);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool CommentsExists(int id)
//        {
//            return (_context.Comments?.Any(e => e.CommentId == id)).GetValueOrDefault();
//        }
//    }
//}
