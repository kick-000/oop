namespace lab2oop;

public abstract class People
{
    private string name;
    private List<Course> Courses = new List<Course>();

    public People(string name)
    {
        this.name = name;
    }
    public string Name
    {
        get => name;
        set  => name = value;
    }
    
    public void AddCourse(Course course)
    {
        if (!Courses.Contains(course))
        {
            Courses.Add(course);
        }
        
    }
    public void DelCourse(Course course)
    {
        if (Courses.Contains(course))
        {
            Courses.Remove(course);
        }
    }
    
    
    public List<Course> GetCourses()
    {
        return Courses;
    }
    
    public void ViewCourses()
    {
        if (Courses.Count == 0)
        {
            Console.WriteLine("Нет назначенных курсов.");
            return;
        }
        foreach (Course c in Courses)
        {
            Console.WriteLine(c.Name + " " + c.Type);
        }
    }
}
