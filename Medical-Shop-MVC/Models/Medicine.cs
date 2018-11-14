using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medical_Shop_MVC.Models
{
    public class Medicine
    {
        public int MedicineID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int Quantity { get; set; }
        public String MedicineCode { get; set; }
    }
}
