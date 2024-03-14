using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Gleb.Models.Messages;

public class LessonClassSubjectMessage(Class value) : ValueChangedMessage<Class>(value)
{
    public Class Class { get; } = value;
}