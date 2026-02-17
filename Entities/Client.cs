using System.ComponentModel.DataAnnotations;

namespace LoginProductMinimalApi.Entities
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string UserName { set; get; }

        [Required]
        public required string Password { set; get; }

        public required string RefreshToken { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
