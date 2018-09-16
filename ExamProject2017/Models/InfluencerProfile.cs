using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Models
{
    public class InfluencerProfile
    {
        [Key, ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string CVR { get; set; }
        public string CPR { get; set; }
        public string AgencyName { get; set; }
        public string BankAccount { get; set; }
        public string WorkDescription { get; set; }
        public int PriceFrom { get; set; }
        public string Characteristic { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<InfluencerPlatform> InfluencerPlatforms { get; set; }
        public virtual ICollection<InfluencerCategory> InfluencerCategory { get; set; }

    }
}
