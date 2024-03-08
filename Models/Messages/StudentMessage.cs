using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Gleb.Models.Messages;

public class StudentMessage : ValueChangedMessage<Student>
{
    public Student Student { get; set; }
    public bool IsEdit { get; set; }
    public int ClassId { get; set; }
    public StudentMessage(Student value, bool? isEdit, int classId) : base(value)
    {
        Student = value;
        ClassId = classId;
        IsEdit = (bool)isEdit!;
    }
}