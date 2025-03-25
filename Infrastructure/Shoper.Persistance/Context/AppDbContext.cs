using Microsoft.EntityFrameworkCore;
using Shoper.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shoper.Persistance.Context
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-ERDEM\\SQLEXPRESS; database=Shoper; Integrated Security=True; TrustServerCertificate=True;");
        }

        public DbSet<Category> Categories { get; set; }
    }
}
