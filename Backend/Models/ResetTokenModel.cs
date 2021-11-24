using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class ResetTokenModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
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
