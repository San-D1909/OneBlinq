using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.In
{
    public class AdminCreateInput
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
