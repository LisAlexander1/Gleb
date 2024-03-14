using Microsoft.EntityFrameworkCore;

namespace Gleb.Models;

[PrimaryKey(nameof(ClassId), nameof(SubjectId))]
public class ClassSubject
{
    public int ClassId { get; set; }
    public virtual Class Class { get; set; }
    public int SubjectId { get; set; }
    public virtual Subject Subject { get; set; }
    public virtual List<Lesson> Lessons { get; set; }
}