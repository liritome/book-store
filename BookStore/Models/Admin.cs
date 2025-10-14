using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [Table("Admins")]
    public class Admin : User
    {
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

        public bool IsActive { get; set; }
    }
}
