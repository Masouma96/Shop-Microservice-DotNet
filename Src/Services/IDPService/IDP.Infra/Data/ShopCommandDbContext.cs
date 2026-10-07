using IDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Infra.Data
{
    public class ShopCommandDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public ShopCommandDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("CommandDBConnection"));
        }

        public DbSet<User> Tbl_Users { get; set; }
       // public DbSet<Outbox> Tbl_Outbox { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
