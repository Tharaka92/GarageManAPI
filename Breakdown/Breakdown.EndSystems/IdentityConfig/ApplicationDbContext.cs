using Breakdown.Contracts.DTOs;
using Breakdown.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Breakdown.EndSystems.IdentityConfig
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
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

        public DbSet<Package> Packages { get; set; }
    }
}
