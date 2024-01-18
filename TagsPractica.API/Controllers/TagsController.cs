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
    public class TagsController : Controller
    {

        private readonly DatabaseContext _context;

        public TagsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Tags
        [HttpGet]
        public async Task<IActionResult> Index2()
        {
            if (_context.Tags != null)
            {
                return StatusCode(200, "Entity set 'DatabaseContext.Tags'  is null.");
            }
            else
            {
                var TagsTemp = await _context.Tags.ToListAsync();
                //return StatusCode(200, TagsTemp);
                return StatusCode(200, "sdas");
            }
        }

        // GET: Tags/Details/5
    }
}
