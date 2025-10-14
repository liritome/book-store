using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class AdminAction
    {
        public int Id { get; set; }

        public int AdminId { get; set; }
        public virtual Admin Admin { get; set; } = null!;
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(256)]
        public string Details { get; set; } = null!;
    }
}
