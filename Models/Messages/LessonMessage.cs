using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Gleb.Models.Messages;

public class LessonMessage(Lesson value) : ValueChangedMessage<Lesson>(value)
{
    public Lesson Lesson { get; set; } = value;
}