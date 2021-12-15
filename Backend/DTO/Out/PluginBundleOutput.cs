using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.Out
{
	public class PluginBundleOutput
	{
		public int Id { get; set; }
		public string BundleName { get; set; }
		public string BundleDescription { get; set; }
		public decimal Price{get; set;}

		public IEnumerable<UserModel> Users { get; set; }
	}

}
