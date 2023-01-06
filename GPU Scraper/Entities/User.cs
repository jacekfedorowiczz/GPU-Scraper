namespace GPUScraper.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }

        public int RoleId { get; set; } = 2;
        public virtual Role Role { get; set; }
    }
}
