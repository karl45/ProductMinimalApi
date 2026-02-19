using System.ComponentModel.DataAnnotations;

namespace LoginProductMinimalApi.Models.Profile
{
    public class UserModel
    {
        public Guid? Id { set; get; }

        [Required]
        public string UserName { set; get; }

        public string ? Password { set; get; }

        public DateTime BirthDate { set; get; }

    }
}
