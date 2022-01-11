using Backend.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.In
{
    public class PluginInput
    {
		public PluginModel GetPluginModel()
        {
			return new PluginModel
			{
				PluginName = this.PluginName,
				PluginDescription = this.PluginDescription,
				MonthlyPrice = this.MonthlyPrice,
				FullPrice = this.FullPrice,
				StripeProductId = this.StripeProductId,
			};
        }
		public string PluginName { get; set; }
		public string PluginDescription { get; set; }
		public decimal MonthlyPrice { get; set; }
		public decimal FullPrice { get; set; }
		public string StripeProductId { get; set; }
		public string FileName { get; set; }
		public string EncodedFileContent { get; set; }
	}
}
