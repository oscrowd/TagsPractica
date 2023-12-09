namespace TagsPractica.DAL.Models
{
    public class PostTag
    {
        public int Id { get; set; }
        public int postId { get; set; }
        public Post Post { get; set; }
        
        public int tagId { get; set; }
        public Tag Tag { get; set; }    
    }
}
