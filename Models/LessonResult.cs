namespace Gleb.Models;

public class LessonResult
{
    public int Id { get; set; }
    public bool IsSkipped { get; set; }
    public int? Mark { get; set; }

    public int LessonId { get; set; }
    public virtual Lesson? Lesson { get; set; }

    public int StudentId { get; set; }
    public virtual Student? Student { get; set; }
}