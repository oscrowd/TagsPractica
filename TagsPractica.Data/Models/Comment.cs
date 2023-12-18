using TagsPractica.DAL.Models;
namespace TagsPractica.DAL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? text { get; set; }

        public int postId { get; set; }
        public Post Post { get; set; }
        //public string userId { get; set; }
        public User User { get; set; }

    }
}
