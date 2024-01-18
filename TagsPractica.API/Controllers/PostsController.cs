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
        public async Task<IActionResult> Posts()
        {
            if (_context.Posts != null)
            {
                return StatusCode(200, "Entity set 'DatabaseContext.Posts'  is null.");
            }
            else
            {
                var PostList = await _context.Posts.Include(m => m.Tags).ToListAsync();
                return StatusCode(200, "dfsdfsdf");
            }
        }
    }
}
