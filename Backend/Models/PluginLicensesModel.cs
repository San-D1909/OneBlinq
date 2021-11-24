using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	[Index(nameof(License), nameof(Plugin), nameof(PluginBundle), IsUnique = true)]

	public class PluginLicensesModel
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public LicenseModel License { get; set; }
		[Required]
		public PluginModel Plugin { get; set; }
		[Required]
		public PluginBundleModel PluginBundle { get; set; }

		[DefaultValue(0)]
		public int TimesActivated { get; set; }
	}
}
