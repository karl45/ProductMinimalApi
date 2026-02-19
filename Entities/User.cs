using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginProductMinimalApi.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string UserName { set; get; }

        [Required]
        public required string Password { set; get; }

        public DateTime? BirthDate { set; get; }

        public string? RefreshToken { get; set; }

        public DateTime? BlockedTime { set; get; }

        public DateTime CreatedAt { get; set; }

        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}
