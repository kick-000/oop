namespace lab2oop;
using System.Collections.Generic;

public class CourseHelpForTest
{
    private List<Course> courses;
    private List<Teacher> teachers;
    private List<Student> students;
    
    public CourseHelpForTest(List<Course> courses, List<Teacher> teachers, List<Student> students)
    {
        this.courses = courses;
        this.teachers = teachers;
        this.students = students;
    }


    public void AdCourse(string courseName,bool onorof)
    {
        Course course = onorof ? new OnlineCourse(courseName) : new OfflineCourse(courseName);
        courses.Add(course);
    }
    public void DeletedCourse(Course course)
    {
        courses.Remove(course);
    }
    
    public void ChoiceCours(Course course,Student student)
    {
        course.AddStudents(student);
    }
    public void TeachCours(Course course, Teacher teacher)
    {
        course.ChTeach(teacher);
    }

    public void AddTeach(string teachName)
    {
        teachers.Add(new Teacher(teachName));
    }
    
    public void AddStu(string studName)
    {
        students.Add(new Student(studName));
    }

    public void AllCoursFromTeach(Teacher teacher, Student student)
    {
        var teachCours = teacher.GetCourses();
        foreach (var course in teachCours)
        {
            course.AddStudents(student);
        }
    }
   
}
