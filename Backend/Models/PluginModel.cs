using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class PluginModel
	{

		[Key]
		public int Id { get; set; }
		[Required]
		public string PluginName { get; set; }
		[Required]
		public string PluginDescription { get; set; }
		[Required]
		public decimal MonthlyPrice { get; set; }
		[Required]
		public decimal FullPrice { get; set; }

		public string StripeProductId { get; set; }
	}
}
