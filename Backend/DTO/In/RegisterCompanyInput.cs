using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.In
{
    public class RegisterCompanyInput
    {
		[Required]
		public string CompanyName { get; set; }
		[Required]
		public string ZipCode { get; set; }
		[Required]
		public string Street { get; set; }
		[Required]
		public string HouseNumber { get; set; }
		[Required]
		public string Country { get; set; }
		public string BTWNumber { get; set; }
		public string KVKNumber { get; set; }
		public string PhoneNumber { get; set; }
	}
}
