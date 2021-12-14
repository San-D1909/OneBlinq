using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class PluginVariantModel
    {
        [Required]
        public PluginModel Plugin { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string StripePriceId { get; set; }
    }
}
