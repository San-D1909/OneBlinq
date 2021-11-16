using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class PluginModel
	{

		[Key]
		public int PluginId { get; set; }
		public string PluginName { get; set; }
		public string PluginDescription { get; set; }

	}
}
