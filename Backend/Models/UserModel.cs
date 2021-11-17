
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class UserModel
	{
		[Key]
		public int UserId { get; set; }
		public string FullName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public bool IsAdmin { get; set; }
	}
}
