using Microsoft.EntityFrameworkCore;

namespace Gleb.Models;

public class JournalDbContext : DbContext
{
    public JournalDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<SchoolSubject> SchoolSubjects { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<LessonResult> LessonResults { get; set; }
}

