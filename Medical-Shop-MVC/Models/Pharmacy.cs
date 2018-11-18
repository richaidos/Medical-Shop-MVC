using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Medical_Shop_MVC.Models
{
    public class Pharmacy
    {
        [Key]
        public int PharmID { get; set; }
        public String PharmName { get; set; }
        public String PharmAddress { get; set; }
        public String PharmPhone { get; set; }
        public String Time_at { get; set; }
    }
}
