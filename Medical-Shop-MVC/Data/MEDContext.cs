using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Medical_Shop_MVC.Models;

namespace Medical_Shop_MVC.Models
{
    public class MEDContext : DbContext
    {
        public MEDContext (DbContextOptions<MEDContext> options)
            : base(options)
        {
        }

        public DbSet<Medical_Shop_MVC.Models.Medicine> Medicine { get; set; }

        public DbSet<Medical_Shop_MVC.Models.Pharmacy> Pharmacy { get; set; }

        public DbSet<Medical_Shop_MVC.Models.Specialization> Specialization { get; set; }

        public DbSet<Medical_Shop_MVC.Models.Medical_Enterprise> Medical_Enterprise { get; set; }

        public DbSet<Medical_Shop_MVC.Models.Doctors> Doctors { get; set; }
    }
}
