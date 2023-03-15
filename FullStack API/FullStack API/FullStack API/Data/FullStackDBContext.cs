using FullStack_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStack_API.Data
{
    public class FullStackDBContext : DbContext
    {
        public FullStackDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
