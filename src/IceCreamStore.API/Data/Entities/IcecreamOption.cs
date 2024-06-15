using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IceCreamStore.API.Data.Entities
{
    public class IcecreamOption
    {
        [Key, Column(Order = 0)]
        public int IcecreamId { get; set; }

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

        public virtual Icecream Icecream { get; set; }
    }
}
