namespace lab2oop;

public abstract class Course
{
    private string name;
    private Teacher teacher;
    private List<Student> students = new List<Student>();
    public abstract string Type
    {
        get;
    }
    
    public Course(string name)
    {
        this.name = name;
    }
    public Course(string name, Teacher teacher)
    {
        this.name = name;
        this.teacher= teacher;
    }
    public Course(string name, Teacher teacher, List<Student> students)
    {
        this.name = name;
        this.teacher = teacher;
        this.students = students;
    }
    public string Name
    {
        get => name;
        set => name = value;
    }
    public void AddStudents(Student student)
    {
        students.Add(student);
        student.AddCourse(this);
    }
    public void ChTeach(Teacher teacher)
    {
        this.teacher = teacher;
        teacher.AddCourse(this);
        
    }
}
