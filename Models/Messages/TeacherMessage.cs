using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Gleb.Models.Messages;

public class TeacherMessage : ValueChangedMessage<Teacher>
{
    public bool IsEdit { get; }
    public Teacher Teacher { get; }
    
    public TeacherMessage(Teacher value, bool? isEdit) : base(value)
    {
        IsEdit = (bool)isEdit!;
        Teacher = value;

    }
}