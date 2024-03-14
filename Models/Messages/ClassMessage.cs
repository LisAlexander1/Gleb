using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Gleb.Models.Messages;

public class ClassMessage : ValueChangedMessage<Class>
{
    public ClassMessage(Class value, bool? isEdit) : base(value)
    {
        Class = value;
        IsEdit = (bool)isEdit!;
    }

    public bool IsEdit { get; }
    public Class Class { get; }
}