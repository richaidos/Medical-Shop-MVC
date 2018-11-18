using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Medical_Shop_MVC.Models
{
    public class Medical_Enterprise
    {
        [Key]
        public int MedID { get; set; }
        public String MedName { get; set; }
        public String MedDescription { get; set; }
        public String MedAddress { get; set; }
        public String Time_at { get; set; }

    }
}
