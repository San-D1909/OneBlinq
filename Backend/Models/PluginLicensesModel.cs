using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class PluginLicensesModel
	{
		[Key]
		public int Id { get; set; }
		public int PluginId { get; set; }
		public int LicenseId { get; set; }
	}
}
