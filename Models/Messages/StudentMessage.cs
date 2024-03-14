using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Gleb.Models.Messages;

public class StudentMessage : ValueChangedMessage<Student>
{
    public StudentMessage(Student value, bool? isEdit, int classId) : base(value)
    {
        Student = value;
        Student.ClassId = classId;
        IsEdit = (bool)isEdit!;
    }

    public Student Student { get; set; }
    public bool IsEdit { get; set; }
}