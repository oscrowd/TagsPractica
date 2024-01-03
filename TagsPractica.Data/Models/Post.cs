using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagsPractica.DAL.Models
{
    public class Post
    {

        
        public int Id { get; set; }
        public string? title { get; set; }
        public string? text { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public Post() 
        {
            Tags = new List<Tag>();
        }

        //public string UserId { get; set; }
        //public virtual User User { get; set; }

        [ForeignKey("User")]
        public string userId { get; set; }
        //public User user { get; set; }

        //public virtual ICollection<User>? Users { get; set; }






    }
}
