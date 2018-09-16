using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Models
{
    public class BusinessProfile
    {
        [Key, ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string BusinessName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int CVR { get; set; }

        [Required]
        public string BusinessType { get; set; }

        [Required]
        public string Industry { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; } 
    }
}
