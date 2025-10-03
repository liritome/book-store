using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [Table("Admins")]
    public class Admin : User
    {
        public ICollection<Role> Roles { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
