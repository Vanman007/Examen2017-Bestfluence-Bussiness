using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject2017.Data
{
    public class Country : BaseEntity
    {
        [MaxLength(2)]
        public string Name { get; set; }
    }
}
