using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.Out
{
	public class DeviceOutput
	{
        public int Id { get; set; }
        public string DeviceToken { get; set; }
        public int LicenseId { get; set; }
        public int PluginId { get; set; }
    }
}
