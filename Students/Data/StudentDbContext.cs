
using Microsoft.EntityFrameworkCore;
using Students.Models;


namespace Students.Data
{
    public class StudentDbContext : DbContext
    {
        //connect to real DB, where the connectionstring is.
        public StudentDbContext(DbContextOptions options) : base(options)
        {

        }
        //Tabel(DbSet) with students
        public DbSet<Student> Students { get; set; }
        // Dev can config the DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // data seeding
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = -1, FirstName = "Winston", LastName = "Muijs", OfYear = 2023, Major = "Software Design"}
            );
        }
    }
}

