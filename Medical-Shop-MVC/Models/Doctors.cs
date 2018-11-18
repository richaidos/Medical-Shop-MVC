using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical_Shop_MVC.Models
{
    public class Doctors
    {
        [Key]
        public int DoctorID { get; set; }
        public String DoctorName { get; set; }
        public String DoctorSurname { get; set; }
        public String DoctorDescription { get; set; }
        public Specialization doctorType { get; set; }
        public String DoctorPhone { get; set; }
        public Medical_Enterprise DoctorEnterprise { get; set; }
        public String Price { get; set; }

    }
}
