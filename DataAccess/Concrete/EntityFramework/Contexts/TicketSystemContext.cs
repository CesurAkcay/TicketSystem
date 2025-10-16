using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class TicketSystemContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }  
        public DbSet<TicketMessage> TicketMessages { get; set; }    
    }
}
