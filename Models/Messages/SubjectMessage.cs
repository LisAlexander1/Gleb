using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Gleb.Models.Messages;

public class SubjectMessage : ValueChangedMessage<Subject>
{
    public SubjectMessage(Subject value, bool? isEdit) : base(value)
    {
        IsEdit = (bool)isEdit!;
        Subject = value;
    }

    public bool IsEdit { get; }
    public Subject Subject { get; }
}