using Atm_sql.configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_sql
{
    public class ApplicationDbcontext:DbContext
    {
        public DbSet<users> users { get; set; }
        public DbSet<TransactionInfo> transactionInfos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=AALAA1\\MSSQLSERVER01;Database=true_Atm;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new userconfiguring());
        }
    }
}
