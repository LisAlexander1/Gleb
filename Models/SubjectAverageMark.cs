using Gleb.Models;

namespace Gleb.Models;

public class SubjectAverageMark
{
    public Subject Subject { get; set; }
    public double Mark { get; set; }
    public double Attendance { get; set; }
}