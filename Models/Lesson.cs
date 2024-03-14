using Microsoft.EntityFrameworkCore;

namespace Gleb.Models;

[PrimaryKey(nameof(Id))]
public class Lesson
{
    public int Id { get; set; }

    public int SubjectId { get; set; }

    public int ClassId { get; set; }

    public int TeacherId { get; set; }

    public virtual ClassSubject ClassSubject { get; set; }

    public virtual TeacherSubject TeacherSubject { get; set; }
    
    public virtual List<LessonResult> LessonResults { get; set; }


    public DateTime Date { get; set; }
}