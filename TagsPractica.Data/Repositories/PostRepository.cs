using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using TagsPractica.DAL.Models;

namespace TagsPractica.DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext _context;

        // Метод-конструктор для инициализации
        public PostRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddPost(Post post)
        {
            
            // Добавление пользователя
            var entry = _context.Entry(post);
            if (entry.State == EntityState.Detached)
                await _context.Posts.AddAsync(post);

            // Сохранение изенений
            await _context.SaveChangesAsync();
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
        public async Task AddComment(Comment comment)
        {

            // Добавление пользователя
            var entry = _context.Entry(comment);
            if (entry.State == EntityState.Detached)
                await _context.Comments.AddAsync(comment);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }

        //public async Task AddPostTag(PostTag postTag)
        //{

            // Добавление пользователя
            //var entry = _context.Entry(postTag);
            //if (entry.State == EntityState.Detached)
                //await _context.PostsTags.AddAsync(postTag);

            // Сохранение изенений
            //await _context.SaveChangesAsync();
        //}
    }
}
