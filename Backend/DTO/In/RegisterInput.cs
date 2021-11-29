using Backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.In
{
	public class RegisterInput
	{

        public RegisterUserInput User { get; set; }
		public RegisterCompanyInput Company { get; set; }

		public bool HasCompany { get; set; }

	}
}
