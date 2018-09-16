using ExamProject2017.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<InfluencerCategory> InfluenterKategori { get; set; }
    }
}
