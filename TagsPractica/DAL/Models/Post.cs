namespace TagsPractica.DAL.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? title { get; set; }
        public string? text { get; set; }
        //public string userId { get; set; }
        public User User { get; set; }
    }
}
