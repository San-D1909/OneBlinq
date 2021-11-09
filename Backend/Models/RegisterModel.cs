using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class RegisterModel
	{
		public string Mail { get; set; }
		public string FullName { get; set; }
		public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string CompanyName { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Country { get; set; }
        public string BTWNumber { get; set; }
        public string KvkNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}
