using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class SchoolDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<TeacherEducations> TeacherEducations { get; set; }
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
         : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Veel op veel relatie
            modelBuilder.Entity<TeacherEducations>((entity) => { entity.HasKey(e => new { e.TeacherId, e.EducationId }); });
        }
    }
}