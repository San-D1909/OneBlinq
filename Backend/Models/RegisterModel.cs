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
		public string UserName { get; set; }
		public string Password { get; set; }
		public string PasswordConfirmation { get; set; }
		public string CompanyName { get; set; }
		public string CompanyAdress { get; set; }
		public string BTWNumber { get; set; }
		public string TelephoneNumber { get; set; }
	}
}
