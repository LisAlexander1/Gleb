using System.ComponentModel.DataAnnotations.Schema;

namespace Gleb.Models;

public class Class
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual List<Student>? Students { get; set; }

    public virtual List<ClassSubject> ClassSubjects { get; set; } = [];
    public virtual List<Subject> Subjects { get; set; } = [];
}