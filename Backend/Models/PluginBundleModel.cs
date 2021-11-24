using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class PluginBundleModel
	{

		[Key]
		public int Id { get; set; }
		[Required]
		public string BundleName { get; set; }
		[Required]
		public string BundleDescription { get; set; }
		[Required]
		public decimal Price { get; set; }
	}
}
