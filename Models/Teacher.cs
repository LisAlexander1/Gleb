namespace Gleb.Models;

public class Teacher : IEntity
{
    
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Surname { get; set; }
    public byte[]? Photo { get; set; }
    
    public int PassportSerial { get; set; }
    public int PassportNumber { get; set; }
    public DateTime BirthDay { get; set; }
    
    public virtual List<Lesson>? Lessons { get; set; }
}