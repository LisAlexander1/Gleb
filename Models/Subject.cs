using System.ComponentModel.DataAnnotations.Schema;
using Gleb.Models.Interfaces;
using Gleb.Models.Messages;

namespace Gleb.Models;

public class Subject : SelectableItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual List<ClassSubject> ClassSubjects { get; set; }
    public virtual List<TeacherSubject> TeacherSubjects { get; set; }
    
    public virtual List<Teacher> Teachers { get; set; }
    public virtual List<Class> Classes { get; set; }

    [NotMapped]
    public double AverageMark => ClassSubjects
        .SelectMany(s =>
            s.Lessons.SelectMany(lesson =>
                lesson.LessonResults.Select(lessonResult =>
                    lessonResult.Mark))).Average() ?? 0;
}