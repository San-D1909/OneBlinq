using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.In
{
	public class DeviceRegisterInput
	{

		public string DeviceInfo { get; set; }
		public string Jtoken { get; set; }

		public string DeviceName { get; set; }
		public string LicenseKey { get; set; }
		public string LocalToken { get; set; }

	}
}
