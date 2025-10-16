using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString("MsSQLConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public TicketSystemContext()
        {

        } //boş constructor

        public TicketSystemContext(DbContextOptions<TicketSystemContext> options) : base(options)
        {

        }  //connectionString bilgisini dışardan alıyoruz.

        public DbSet<Customer> Customers { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }  
        public DbSet<TicketMessage> TicketMessages { get; set; }    
    }
}
