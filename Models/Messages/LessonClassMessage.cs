using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Gleb.Models.Messages;

public class LessonClassMessage : ValueChangedMessage<Class>
{
    public Class Class { get; set; }
    public LessonClassMessage(Class value) : base(value)
    {
        Class = value;
    }
}