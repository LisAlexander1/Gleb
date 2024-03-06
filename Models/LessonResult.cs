namespace Gleb.Models;

public class LessonResult : IEntity
{
    public int Id { get; set; }
    public bool IsSkipped { get; set; }
    public Marks Mark { get; set; }
    
    public int LessonId { get; set; }
    public virtual Lesson? Lesson { get; set; }
    
    public int StudentId { get; set; }
    public virtual Student? Student { get; set; }
}