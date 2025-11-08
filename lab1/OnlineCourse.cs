namespace lab2oop;

public class OnlineCourse : Course
{
    public OnlineCourse(string name) : base(name) {}
    public override string Type => "Online";
}
