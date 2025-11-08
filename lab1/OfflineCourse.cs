namespace lab2oop;

public  class OfflineCourse : Course
{
    public OfflineCourse(string name) : base(name) {}
    public override string Type => "Offline";
}
