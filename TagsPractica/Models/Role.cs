namespace TagsPractica.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string roleName { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }


    }
}
