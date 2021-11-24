using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Index(nameof(PluginBundle), nameof(Plugin), IsUnique = true)]
    public class PluginBundles
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public PluginBundleModel PluginBundle { get; set; }
        [Required]
        public PluginModel Plugin { get; set; }
    }
}
