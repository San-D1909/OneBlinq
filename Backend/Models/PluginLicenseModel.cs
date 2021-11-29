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
	[Index(nameof(LicenseId), nameof(PluginId), nameof(PluginBundleId), IsUnique = true, Name = "IX_UNIQUE_PLUGINLICENSE")]

	public class PluginLicenseModel
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int LicenseId { get; set; }
		public LicenseModel License { get; set; }
		[Required]
		public int PluginId { get; set; }
		public PluginModel Plugin { get; set; }
		[Required]
		public int PluginBundleId { get; set; }
		public PluginBundleModel PluginBundle { get; set; }

		[DefaultValue(0)]
		public int TimesActivated { get; set; }
	}
}
