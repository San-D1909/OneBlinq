using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Index(nameof(PluginBundleId), nameof(PluginId), IsUnique = true, Name = "IX_UNIQUE_PLUGINBUNDLE")]
    public class PluginBundlesModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PluginBundleId { get; set; }
        public PluginBundleModel PluginBundle { get; set; }
        [Required]
        public int PluginId { get; set; }
        public PluginModel Plugin { get; set; }
    }
}
