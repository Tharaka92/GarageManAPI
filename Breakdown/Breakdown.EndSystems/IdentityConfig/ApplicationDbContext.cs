using Breakdown.Contracts.DTOs;
using Breakdown.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Breakdown.EndSystems.IdentityConfig
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        private readonly IOptions<ConnectionStringDto> _connectionString;

        public ApplicationDbContext(IOptions<ConnectionStringDto> connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString.Value.BreakdownDb);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                 .SelectMany(t => t.GetProperties())
                                                 .Where(p => p.ClrType == typeof(decimal)))
            {
                property.Relational().ColumnType = "decimal(15, 2)";
            }

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Package> Packages { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
