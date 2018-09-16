using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Models.BusinessViewModels
{
    public class EditViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Firstname is required.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Business name is required.")]
        public string BusinessName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "CVR is required.")]
        public int CVR { get; set; }

        [Required(ErrorMessage = "Business type is required.")]
        public string BusinessType { get; set; }

        [Required(ErrorMessage = "Industry is required.")]
        public string Industry { get; set; }
    }
}
