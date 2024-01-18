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
        [Route("GetTags")]
        public async Task<IActionResult> Index()
        {
              return _context.Tags != null ? 
                          StatusCode(200, await _context.Tags.ToListAsync()) :
                          StatusCode(200,"Entity set 'DatabaseContext.Tags'  is null.");
        }

        [HttpPost]
        [Route("CreateTag")]
        public async Task<IActionResult> Create([Bind("Id,title,text")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return StatusCode(200, "Tag создан");
            }
            return StatusCode(400, "Данные не валидны");
        }

        
        [HttpPatch]
        [Route("EditTag")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,title,text")] Tag tag)
        {
            if (id != tag.Id)
            {
                return StatusCode(200, "Не найден");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                    return StatusCode(200, "Tag Отредактирован");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(tag.Id))
                    {
                        return StatusCode(200, "Не найден");
                    }
                    else
                    {
                        return StatusCode(500, "Не известная ошибка");
                    }
                }
                
            }
            return StatusCode(200, "Данные не валидны");
        }

       
        // POST: Tags/Delete/5
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tags == null)
            {
                return StatusCode(200,"Entity set 'DatabaseContext.Tags'  is null.");
            }
            var tag = await _context.Tags.FindAsync(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
            }
            
            await _context.SaveChangesAsync();
            return StatusCode (200, "Tag удален");
        }

        private bool PostExists(int id)
        {
          return (_context.Tags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
