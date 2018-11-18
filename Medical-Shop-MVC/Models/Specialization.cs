using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Medical_Shop_MVC.Models
{
    public class Specialization
    {
        [Key]
        public int SpecID { get; set; }
        public String SpecName { get; set; }
        public String SpecDescription { get; set; }
    }
}
