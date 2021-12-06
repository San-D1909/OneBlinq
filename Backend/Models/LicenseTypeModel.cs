using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class LicenseTypeModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TypeName { get; set; }
        [Required]
        public int MaxAmount { get; set; }
        [Required]
        public int MonthlyPeriod { get; set; }
        private DateTime? _creationTime = null;
        [Required]
        public DateTime CreationTime
        {
            get
            {
                return this._creationTime.HasValue
                    ? this._creationTime.Value
                    : DateTime.Now;
            }
            set
            {
                this._creationTime = value;
            }
        }
    }
}
