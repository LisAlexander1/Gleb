using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Gleb.Models.Messages;

public class ClassMessage : ValueChangedMessage<Class>
{
    public bool IsEdit { get; }
    public Class Class { get; }
    
    public ClassMessage(Class value, bool? isEdit) : base(value)
    {
        Class = value;
        IsEdit = (bool)isEdit!;
    }
}