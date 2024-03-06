namespace Gleb.Models;

public class Lesson : IEntity
{
    public int Id { get; set; }
    
    public int ClassId { get; set; }
    public virtual Class? Class { get; set; }
    
    public int SchoolSubjectId { get; set; }
    public virtual SchoolSubject? SchoolSubject { get; set; }
    
    public virtual int TeacherId { get; set; }
    public virtual Teacher? Teacher { get; set; }
    
    public DateTime Date { get; set; }
}