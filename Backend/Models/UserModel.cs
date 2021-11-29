
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class UserModel { 
		[Key]
		public int Id { get; set; }
		[Required]
		public string FullName { get; set; }
		[Required]
		public string Email { get; set; }
		[DefaultValue(false)]
        public bool IsVerified { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
        public byte[] Salt { get; set; }
		[DefaultValue(false)]
		public bool IsAdmin { get; set; }
		public CompanyModel Company { get; set; }
    }
}
