
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.Out
{
	public class LicenseOutput
	{

		public int Id { get; set; }
		
		public UserModel User { get; set; }

		public LicenseModel License { get; set; }

		public LicenseTypeModel LicenseType { get; set; }

		public int? PluginId { get; set; }

		public PluginModel Plugin { get; set; } = null;

		public int? PluginBundleId { get; set; }

		public PluginBundleModel PluginBundle { get; set; } = null;

		public int TimesActivated { get; set; }

		public IEnumerable<DeviceModel> Devices { get; set; }

	}
}
