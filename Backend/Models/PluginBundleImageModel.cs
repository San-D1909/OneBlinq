using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class PluginBundleImageModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public PluginBundleModel PluginBundle { get; set; }
        [Required]
        [Column("imagedata", TypeName = "LONGTEXT")]
        public string ImageData { get; set; }
    }
}
