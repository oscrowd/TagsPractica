using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TagsPractica.DAL;
using TagsPractica.DAL.Models;
using TagsPractica.API.ViewModels;


namespace TagsPractica.API.Controllers

{
    [ApiController]
    [Route("Controller")]
    public class PostsController : Controller
    {
        private readonly DatabaseContext _context;

        public PostsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Posts
        [HttpGet]
        [Route("GetPosts")]
        public async Task<IActionResult> GetPosts()
        {
            if (_context.Posts != null)
            {
                //var PostList = await _context.Posts.
                //
                //
                //
                //
                //(m => m.Tags).ToListAsync();
                var PostList = await _context.Posts.ToListAsync();
                return StatusCode(200, PostList);
            }
            else
            {
                return StatusCode(200, "Entity set 'DatabaseContext.Posts'  is null.");
            }
        }

        [HttpPost]
        [Route("CreatePost")]
        public async Task<IActionResult> Create([Bind("Id,title,text")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return StatusCode(200,"Пост создан");
            }
            return StatusCode(400, "Неправиильный формат данных");
        }

        [HttpPatch]
        [Route("EditPost")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,title,text")] Post post)
        {
            if (id != post.Id)
            {
                return StatusCode(200, "Пост не найден");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return StatusCode(200, "Пост не найден");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return StatusCode(200, post);
        }
        private bool PostExists(int id)
        {
            return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        [HttpDelete]
        [Route("DeletePost")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posts == null)
            {
                return StatusCode(200,"Entity set 'DatabaseContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return StatusCode(200, "Пост удален");
            }
            else { return StatusCode(200, "Запись не найдена"); }
           
        }
    }
}
