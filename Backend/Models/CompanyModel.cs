using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class CompanyModel
    {
		[Key]
		public int Id { get; set; }
		public string CompanyName { get; set; }
		public string ZipCode { get; set; }
		public string Street { get; set; }
		public string HouseNumber { get; set; }
		public string Country { get; set; }
		public string BTWNumber { get; set; }
		public string KVKNumber { get; set; }
		public string PhoneNumber { get; set; }
	}
}
