using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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

	}
}
