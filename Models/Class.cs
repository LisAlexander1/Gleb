namespace Gleb.Models;

public class Class : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public virtual List<Student>? Students { get; set; }
    public virtual List<Lesson>? Lessons { get; set; }
}