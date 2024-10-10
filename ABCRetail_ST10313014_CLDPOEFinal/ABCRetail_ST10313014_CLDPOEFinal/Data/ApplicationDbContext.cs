using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ABCRetail_ST10313014_CLDPOEFinal.Models;

namespace ABCRetail_ST10313014_CLDPOEFinal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ABCRetail_ST10313014_CLDPOEFinal.Models.Cart> Cart { get; set; } = default!;
        public DbSet<ABCRetail_ST10313014_CLDPOEFinal.Models.Customer> Customer { get; set; } = default!;
        public DbSet<ABCRetail_ST10313014_CLDPOEFinal.Models.Product> Product { get; set; } = default!;
    }
}
