using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class AdminPermission
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}