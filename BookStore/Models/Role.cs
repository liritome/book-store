using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public virtual ICollection<AdminPermission> Permissions { get; set; } = new List<AdminPermission>();
    }
}