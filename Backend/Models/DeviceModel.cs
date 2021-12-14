using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Index(nameof(LicenseId), nameof(DeviceToken), IsUnique = true, Name = "IX_UNIQUE_DEVICE")]
    public class DeviceModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DeviceToken { get; set; }
        [Required]
        public int LicenseId { get; set; }
    }
}
