using Backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.In
{
	public class UpdateUserInput
	{

		public UserModel User { get; set; }
		public CompanyModel Company { get; set; }

	}
}
