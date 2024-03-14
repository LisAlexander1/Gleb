namespace Gleb.Models;

public class Teacher : Person
{
    public int Id { get; set; }

    public virtual List<TeacherSubject> TeacherSubjects { get; set; }
    public virtual List<Subject> Subjects { get; set; }
}