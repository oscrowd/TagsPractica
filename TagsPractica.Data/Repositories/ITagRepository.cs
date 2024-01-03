using TagsPractica.DAL.Models;

namespace TagsPractica.DAL.Repositories
{
    public interface ITagRepository
    {
        Task AddTag(Tag tag);
        //Task AddComment(Comment comment);
        Task AddPostTag(PostTag postTag);
    }
}
