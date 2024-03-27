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
                return NotFound("No comments found.");
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
                return NotFound("Comment not found."); ;
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
                return NotFound("Comment not found.");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Comment deleted successfully." });
        }


        // GET: api/Comments/Product/5
        [HttpGet("Product/{productId}")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetCommentsByProductId(int productId)
        {
            var comments = await _context.Comments.Where(c => c.ProductId == productId).ToListAsync();
            if (comments == null || !comments.Any())
            {
                return NotFound("No comments found for the specified product ID.");
            }
            return comments;
        }
    }
}
