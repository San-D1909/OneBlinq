using Backend.Infrastructure.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.In
{
	public class PluginBundleInput
	{
		public PluginBundleModel GetPluginBundleModel()
        {
			return new PluginBundleModel
			{
				Id = this.Id,
				BundleName = this.BundleName,
				BundleDescription = this.BundleDescription,
				StripeProductId = this.StripeProductId
			};
        }
		public int Id { get; set; }
		public string BundleName { get; set; }
		public string BundleDescription { get; set; }
		public string StripeProductId { get; set; }
		public IEnumerable<int> PluginIds { get; set; }
		public string FileName { get; set; }
		public string EncodedFileContent { get; set; }
	}
}
