using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class GumroadResponse
	{
	
		[ModelBinder(Name = "license_key")]
		public string License_Key { get; set; }

		[ModelBinder(Name = "email")]
		public string Email{ get; set; }

		[ModelBinder(Name = "full_name")]
		public string Full_Name{ get; set; }

		[ModelBinder(Name = "variants")]
		public string Variants { get; set; }

		[ModelBinder(Name = "sale_timestamp")]
		public string Creation_Time { get; set; }
	}
}
