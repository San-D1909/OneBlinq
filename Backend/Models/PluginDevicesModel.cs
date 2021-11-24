using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Index(nameof(License), nameof(Plugin), nameof(MacAddress), IsUnique = true)]
    public class PluginDevicesModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MacAddress { get; set; }
        [Required]
        public LicenseModel License { get; set; }
        [Required]
        public PluginModel Plugin { get; set; }
    }
}
