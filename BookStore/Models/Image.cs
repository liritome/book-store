using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        [Url]
        public string Url { get; set; } = null!;
    }
}
