using System.ComponentModel.DataAnnotations;

namespace IceCreamStore.API.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; }
        public int IcecreameId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Range(0.1, double.MaxValue)]
        public double Price { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }

        /// <summary>
        /// Аромат
        /// </summary>
        [Required, MaxLength(50)]
        public string Flavor { get; set; }

        /// <summary>
        /// Начинка
        /// </summary>
        [Required, MaxLength(50)]
        public string Topping { get; set; }

        [Range(0.1, double.MaxValue)]
        public double TotalPrice { get; set; }

        public virtual Order Order { get; set; }
    }
}
