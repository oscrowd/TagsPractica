using System.ComponentModel.DataAnnotations.Schema;

namespace TagsPractica.DAL.Models




{
    public class User
    {
        public Guid Id { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }

       
        public int roleId { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<Role>? Roles { get; set; }


    }
}
