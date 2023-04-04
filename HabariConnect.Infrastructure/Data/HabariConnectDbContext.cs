using HabariConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabariConnect.Infrastructure.Data
{
    public class HabariConnectDbContext : DbContext
    {
        
        public HabariConnectDbContext(DbContextOptions<HabariConnectDbContext> options) : base(options)
        {
           
        }
        public DbSet<User> Users { get; set; }
    }
}
