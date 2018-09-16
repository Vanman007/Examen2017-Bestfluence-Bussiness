using ExamProject2017.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Models
{
    public class InstagramCountry : BaseEntity
    {
        public string InstagramDataId { get; set; }
        public virtual InstagramData InstagramData { get; set; }

        public string CountryId { get; set; }
        public virtual Country Country { get; set; }

        public int Count { get; set; }
    }
}
