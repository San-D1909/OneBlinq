using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class LicenseModel
	{
		[Key]
        public int Id { get; set; }
        [Required]
		public string LicenseKey { get; set; }
		[Required]
		public UserModel User { get; set; }
		public LicenseTypeModel LicenseType { get; set; }
        public int LicenseTypeId { get; set; }
		[Required]
		public bool IsActive { get; set; }
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
        [Required]
        public DateTime ExpirationTime { get; set; }
        [Required]
		public int TimesActivated { get; set; }
	}
}
