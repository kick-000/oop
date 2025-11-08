namespace lab2oop;

class CourseHelp
{
    private List<Course> courses;
    private List<Teacher> teachers;
    private List<Student> students;

    private bool TryError(Course cours)
    {
        if (cours == null)
        {
            Console.WriteLine("Повторите попытку позже!!!");
            return true;
        }
        return false;
    }
    public CourseHelp(List<Course> courses, List<Teacher> teachers, List<Student> students)
    {
        this.courses = courses;
        this.teachers = teachers;
        this.students = students;
    }


    public void AdCourse()
    {
        Console.WriteLine("Введите название курса: ");
        string courseName = Console.ReadLine();
        Console.WriteLine("Введите тип курса: ");
        Console.WriteLine("             1.онлайн");
        Console.WriteLine("             2.офлайн");
        
        if (!int.TryParse(Console.ReadLine(), out int typeCourse))
        {
            Console.WriteLine("Ошибка ввода!");
            return;
        }

        switch (typeCourse)
        {
            case 1:
                courses.Add(new OnlineCourse(courseName));
                break;
            case 2:
                courses.Add(new OfflineCourse(courseName));
                break;
            default:
                Console.WriteLine("Неверное значение, попробуйте снова.");
                break;
        }
    }
    public void DeletedCourse()
    {
        var course = findCourse();
        if (TryError(course))
        {
            return;   
        }
        courses.Remove(course);
        Console.WriteLine("Вы удалили курс " + course.Name);

    }
    public void TeachCours()
    {
        var course = findCourse();
        if (TryError(course))
        {
            return;   
        }

        var teacher = findTeacher();
        if (teacher == null)
        {
            Console.WriteLine("Преподаватель не найден!!!");
            return;
        }

        
        course.ChTeach(teacher);
    }
    public void ChoiceCours()
    {
        var cours = findCourse();
        if (TryError(cours))
        {
            return;   
        }

        var studant = findStudent();
        if (studant == null)
        {
            Console.WriteLine("Студент не найден!");
            return;
        }
        cours.AddStudents(studant);
    }
    public void AddTeach()
    {
        Console.WriteLine("Введите фио преподавателя: ");
        string teachName = Console.ReadLine();
        teachers.Add(new Teacher(teachName));
    }
    public void AddStu()
    {
        Console.WriteLine("Введите фио студента: ");
        string studName = Console.ReadLine();
        students.Add(new Student(studName));
    }

    public void AllCoursFromTeach()
    {
        var teacher = findTeacher();
        if (teacher == null)
        {
            Console.WriteLine("Преподаватель не найден!!!");
            return;
        }

        var teachCours = teacher.GetCourses();

        if (teachCours.Count == 0)
        {
            Console.WriteLine("У преподавателя нет курсов");
            return;
        }
        var student =  findStudent();
        if (student == null)
        {
            Console.WriteLine("Студет не найден!!!");
            return;
        }
        
        Console.WriteLine("Курсы " + teacher.Name + ": ");
        teacher.ViewCourses();
        foreach (var course in teachCours)
        {
            course.AddStudents(student);
        }
        Console.WriteLine("Все курсы успешно добавлены");
    }
    public void AllInfo()
    {
        Console.WriteLine("Курсы: ");
        for (int i = 0; i < courses.Count; i++)
        {
            Console.WriteLine($"{i + 1} . {courses[i].Name} ");
        }
        Console.WriteLine("----------------------");
        Console.WriteLine("Преподаватели: ");
        for (int i = 0; i < teachers.Count; i++)
        {
            Console.WriteLine($"{i + 1} . {teachers[i].Name}");
        }
        Console.WriteLine("----------------------");
        Console.WriteLine("Студенты: ");
        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine($"{i + 1} . {students[i].Name}");
        }
        Console.WriteLine("----------------------");
    }
    public Course findCourse()
    {
        if (courses.Count == 0)
        {
            Console.WriteLine("Нет доступных курсов.");
            return null;
        }
        for (int i = 0; i < courses.Count; i++)
        {
            Console.WriteLine($"{i + 1} . {courses[i].Name}");
        }

        Console.WriteLine("Введите номер курса:");
        if (!int.TryParse(Console.ReadLine(), out int numcours))
        {
            Console.WriteLine("Ошибка ввода!");
            return null;
        }

        if (numcours < 1 || numcours > courses.Count)
        {
            Console.WriteLine("Введен неверный номер!");
            return null;
        }

        return courses[numcours - 1];
    }
    public Teacher findTeacher()
    {
        if (teachers.Count == 0)
        {
            Console.WriteLine("Нет доступных преподавателей .");
            return null;
        }
        Console.WriteLine("Преподаватели:");
        for (int i = 0; i < teachers.Count; i++)
        {
            Console.WriteLine($"{i + 1} . {teachers[i].Name}");
        }

        Console.WriteLine("Введите номер преподавателя:");
        if (!int.TryParse(Console.ReadLine(), out int numteach))
        {
            Console.WriteLine("Ошибка ввода!");
            return null;
        }

        if (numteach < 1 || numteach > teachers.Count)
        {
            Console.WriteLine("Введен неверный номер!");
            return null;
        }

        return teachers[numteach - 1];
    }
    public Student findStudent()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("Нет доступных студентов .");
            return null;
        }
        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine($"{i + 1} . {students[i].Name}");
        }

        Console.WriteLine("Введите номер студента:");
        if (!int.TryParse(Console.ReadLine(), out int numstud))
        {
            Console.WriteLine("Ошибка ввода!");
            return null;
        }

        if (numstud < 1 || numstud > students.Count)
        {
            Console.WriteLine("Введен неверный номер!");
            return null;
        }

        return students[numstud - 1];
    }
}
