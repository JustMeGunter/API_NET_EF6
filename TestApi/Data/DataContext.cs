using Microsoft.EntityFrameworkCore;
using Test1API.Models;

namespace Test1API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) { }

        public DbSet<test> test { get; set; }
    }
}