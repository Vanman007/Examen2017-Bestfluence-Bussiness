using ExamProject2017.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Models
{
    public class InfluencerCategory : BaseEntity
    {
        [ForeignKey("InfluencerProfile")]
        public string InfluencerProfileId { get; set; }
        public InfluencerProfile InfluencerProfile { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
