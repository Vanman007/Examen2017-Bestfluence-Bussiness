using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Models
{
    public class ListObject
    {
        public string Id{ get; set; } 
        public string InfluencerListId { get; set; }
        public virtual InfluencerList InfluencerList { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
