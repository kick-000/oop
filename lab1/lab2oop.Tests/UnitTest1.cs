namespace lab2oop.Tests
{
    namespace UnitTest1
    {
        public class CourseHelpForTestTests
        {
            [Fact]
            public void Test()
            {
                var courses = new List<Course>();
                var teachers = new List<Teacher>();
                var students = new List<Student>();
                var courseHelp = new CourseHelpForTest(courses, teachers, students);
                
                courseHelp.AddStu("Иван Петров");
                courseHelp.AddTeach("Анна Сидорова");
                courseHelp.AdCourse("Программирование C#", true); 

                var student = students[0];
                var teacher = teachers[0];
                var course = courses[0];
                courseHelp.ChoiceCours(course, student);

            }
        }
    }
}

