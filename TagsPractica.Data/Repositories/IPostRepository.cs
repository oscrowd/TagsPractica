using TagsPractica.DAL.Models;

namespace TagsPractica.DAL.Repositories
{
    public interface IPostRepository
    {
        Task AddPost(Post post);
        Task AddTag(Tag tag);
        Task AddComment(Comment comment);
        Task AddPostTag(PostTag postTag);
    }
}
