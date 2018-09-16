using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Models.InfluencerViewModels
{
    public class EditViewModel
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Address { get; set; }

        public string CVR { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string CPR { get; set; }

        [Display(Name = "Agency name")]
        public string AgencyName { get; set; }

        [Display(Name = "Bank account number")]
        [Required]
        public string BankAccount { get; set; }

        [Display(Name = "Work description")]
        [Required]
        public string WorkDescription { get; set; }

        [Display(Name = "Starting price")]
        [Required]
        public int PriceFrom { get; set; }
        public string Characteristic { get; set; }

        public bool LifeStyle { get; set; }

        public bool Fashion { get; set; }

        public bool Entertainment { get; set; }

        public bool Personal { get; set; }

        public bool Gaming { get; set; }

        public bool Interests { get; set; }

        public bool ProfileIsCreated { get; set; }
    }
}
