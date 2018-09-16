using ExamProject2017.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Models
{
    public class InfluencerPlatform : BaseEntity
    {
        [ForeignKey("ApplicationUser")]
        public string InfluencerProfileId { get; set; }
        public InfluencerProfile ApplicationUser { get; set; }
        
        public string PlatformId { get; set; }
        public Platform Platform { get; set; }

    }
}
