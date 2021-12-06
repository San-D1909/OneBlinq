using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTO.In
{
    public class UserinfoDTO
    {
        public UserModel User { get; set; }
        public CompanyModel Company { get; set; }
        public int UserID { get; set; }
    }
}
