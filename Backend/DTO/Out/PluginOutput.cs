using Backend.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.Out
{
    public class PluginOutput
    {
		public int Id { get; set; }
		public string PluginName { get; set; }
		public string PluginDescription { get; set; }
		public decimal FullPrice { get; set; }
		public decimal MonthlyPrice { get; set; }
		public IEnumerable<UserModel> Users { get; set; } 
		public PluginImageModel Image { get; set; }
	}
}
