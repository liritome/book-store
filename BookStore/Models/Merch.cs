using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Merch : Product
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(3000)]
        public string FullDescription { get; set; } = null!;

        public int Stock {  get; set; }

        public override string Name => FullName;

        public override string Description => FullDescription;
    }
}
