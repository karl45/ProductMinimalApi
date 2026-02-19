namespace LoginProductMinimalApi.Entities
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        
        public required User User { get; set; }
        
        public long RoleId { get; set; }    

        public required Role Role { get; set; }
    }
}
