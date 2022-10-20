using Microsoft.EntityFrameworkCore;
using TestTask1.Infrastructure.Models;

namespace TestTask1.ApiDb
{
    public class ApiDbContext : DbContext
    {
        #region *** Сущности ***

        public DbSet<Employee> Employees { get; set; }

        #endregion 

        public ApiDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // задаём уникальность для столбца Name (ФИО)
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}
