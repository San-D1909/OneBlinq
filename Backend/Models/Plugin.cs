using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class Plugin
	{

		[Key]
		public int Id { get; set; }
		public string PluginName { get; set; }
		public string PluginDescription { get; set; }

		[Column("price")]
		public decimal Price { get; set; }

	}
}
