using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class License
	{
		[Key]
		public string LicenseId { get; set; }
		public int UserId { get; set; }
		public string LicenseType { get; set; }
		public bool IfActive { get; set; }
		public DateTime CreationTime { get; set; }
		public int TimesActivated { get; set; }
		public DateTime ExpirationDate { get; set; }
	}
}
