using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Gleb.Models.Messages;

public class LessonMessage : ValueChangedMessage<Lesson>
{
    public Lesson Lesson { get; set; }
    public LessonMessage(Lesson value) : base(value)
    {
        Lesson = value;
    }
}