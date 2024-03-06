namespace Gleb.Models;

public class Student : IEntity
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Surname { get; set; }
    public byte[]? Photo { get; set; }
    
    public int PassportSerial { get; set; }
    public int PassportNumber { get; set; }
    public DateTime BirthDay { get; set; }
    
    public int ClassId { get; set; }
    public virtual Class? Class { get; set; }
    
    public virtual List<LessonResult>? LessonResults { get; set; }
}