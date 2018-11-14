using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Medical_Shop_MVC.Models
{
    public class MEDContext : DbContext
    {
        public MEDContext (DbContextOptions<MEDContext> options)
            : base(options)
        {
        }

        public DbSet<Medical_Shop_MVC.Models.Medicine> Medicine { get; set; }
    }
}
