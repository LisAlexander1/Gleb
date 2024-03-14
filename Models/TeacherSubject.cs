using Microsoft.EntityFrameworkCore;

namespace Gleb.Models;

[PrimaryKey(nameof(TeacherId), nameof(SubjectId))]
public class TeacherSubject
{
    public int TeacherId { get; set; }
    public virtual Teacher Teacher { get; set; }
    public int SubjectId { get; set; }
    public virtual Subject Subject { get; set; }
    public virtual List<Lesson> Lessons { get; set; }
}