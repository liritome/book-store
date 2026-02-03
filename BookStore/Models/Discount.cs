using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public abstract class Discount
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public abstract int Apply(int basePrice);

        public abstract string Display();
    }

    public class PercentageDiscount : Discount
    {
        [Range(1, 100)]
        public int Percentage { get; set; }

        public override int Apply(int basePrice)
        {
            return basePrice * (1 -  Percentage / 100);
        }

        public override string Display()
        {
            return $"-{Percentage} %";
        }
    }

    public class AmountDiscount : Discount
    {
        public int Amount {  get; set; }

        public override int Apply(int basePrice)
        {
            if (Amount >= basePrice) return 1;
            else return basePrice - Amount;
        }

        public override string Display()
        {
            return $"-{Amount} ₴";
        }
    }
}
