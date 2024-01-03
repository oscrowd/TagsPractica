using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using TagsPractica.DAL.Models;

namespace TagsPractica.DAL.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _context;

        // Метод-конструктор для инициализации
        public TagRepository(DatabaseContext context)
        {
            _context = context;
        }

        

        public async Task AddTag(Tag tag)
        {

            // Добавление пользователя
            var entry = _context.Entry(tag);
            if (entry.State == EntityState.Detached)
                await _context.Tags.AddAsync(tag);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }

        public async Task AddPostTag(PostTag postTag)
        {

        // Добавление пользователя
        var entry = _context.Entry(postTag);
        if (entry.State == EntityState.Detached)
        await _context.PostTag.AddAsync(postTag);

        // Сохранение изенений
        await _context.SaveChangesAsync();
        }
    }
}
