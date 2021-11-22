using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class RegisterModel
	{

        public RegisterUserModel User { get; set; }
		public RegisterCompanyModel Company { get; set; }

    }
}
