namespace TagsPractica.DAL.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string text { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public Tag() 
        {
            Posts = new List<Post>();
        }
    }
}
