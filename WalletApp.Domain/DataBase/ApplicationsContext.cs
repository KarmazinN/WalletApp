using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApp.Domain.DbModels;

namespace WalletApp.Domain.DataBase
{
    public class ApplicationsContext : DbContext
    {
        public ApplicationsContext()
        {

        }
        public ApplicationsContext(DbContextOptions<ApplicationsContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Icon> Icons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DbCoreConnectionString");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(
                entity =>
                {
                    entity.ToTable("users");

                    entity.HasKey(k => k.Id);
                });


            modelBuilder.Entity<Transaction>(
                entity =>
                {
                    entity.ToTable("transaction");

                    entity.HasKey(p => p.Id);
                });
        }
    }
}
