using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginProductMinimalApi.Entities
{
    public class Role
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<UserRole> Users { get; set; } = new List<UserRole>();
    }
}
