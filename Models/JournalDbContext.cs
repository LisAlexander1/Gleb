using Microsoft.EntityFrameworkCore;

namespace Gleb.Models;

public class JournalDbContext : DbContext
{
    public JournalDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<ClassSubject> ClassSubjects { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<LessonResult> LessonResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassSubject>()
            .HasMany<Lesson>(e => e.Lessons)
            .WithOne(e => e.ClassSubject)
            .HasForeignKey(e => new { e.ClassId, e.SubjectId });

        modelBuilder.Entity<TeacherSubject>()
            .HasMany<Lesson>(e => e.Lessons)
            .WithOne(e => e.TeacherSubject)
            .HasForeignKey(e => new { e.TeacherId, e.SubjectId });

        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Subjects)
            .WithMany(s => s.Teachers)
            .UsingEntity<TeacherSubject>();
        
        modelBuilder.Entity<Class>()
            .HasMany(t => t.Subjects)
            .WithMany(s => s.Classes)
            .UsingEntity<ClassSubject>();
    }
}