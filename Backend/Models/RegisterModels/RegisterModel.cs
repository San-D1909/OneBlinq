using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class RegisterModel
	{
        [Key]
        public int ModelId { get; set; }
        public RegisterUserModel user { get; set; }
		public RegisterCompanyModel company { get; set; }
    }
}
