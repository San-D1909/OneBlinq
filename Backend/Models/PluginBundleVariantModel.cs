using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class PluginBundleVariantModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PluginId { get; set; }
        public PluginModel Plugin { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string StripePriceId { get; set; }

        [Required]
        public int MaxActivations { get; set; }

        [Required]
        public bool IsSubscription { get; set; }
    }
}
