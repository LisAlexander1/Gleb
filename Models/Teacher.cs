namespace Gleb.Models;

public class Teacher : Person, IEntity
{
    
    public int Id { get; set; }
    
    
    
    public virtual List<Lesson>? Lessons { get; set; }
}