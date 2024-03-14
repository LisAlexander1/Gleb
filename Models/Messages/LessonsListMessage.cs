using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Gleb.Models.Messages;

public class LessonsListMessage(Class @class, Subject subject) : ValueChangedMessage<Class>(@class)
{
    public Class Class { get; } = @class;
    public Subject Subject { get; } = subject;
}