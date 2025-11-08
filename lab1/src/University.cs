namespace lab2oop;

class University
{
    private CourseHelp help;
    List<Course> courses = new();
    List<Teacher> teachers = new();
    List<Student> students = new();

    public University()
    {
        help = new CourseHelp(courses, teachers,students);
    }
    public void Start()
    {
        while (true)
        {
            Console.WriteLine("Введите номер операции:");
            Console.WriteLine("1. Добавить курс.");
            Console.WriteLine("2. Удалить курс.");
            Console.WriteLine("3. Назначить преподавателя на крус.");
            Console.WriteLine("4. Назаначить студенту курс.");
            Console.WriteLine("5. Назначить студенту все курсы преподователя.");
            Console.WriteLine("6. Добавить нового преподавателя.");
            Console.WriteLine("7. Добавить нового студента.");
            Console.WriteLine("8. Вся информация.");
            Console.WriteLine("0. Завершить работу.");
            Console.WriteLine("                     ");
            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine("Ошибка ввода!");
                return;
            }
            switch (number)
            {
                case 1:
                    help.AdCourse();//ok
                    break;
                case 2:
                    help.DeletedCourse(); //ok
                    break;
                case 3:
                    help.TeachCours();
                    break;
                case 4:
                    help.ChoiceCours();
                    break;
                case 5:
                    help.AllCoursFromTeach();
                    break;
                case 6:
                    help.AddTeach();
                    break;
                case 7:
                    help.AddStu();
                    break;
                case 8:
                    help.AllInfo();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Неверное значение, попробуйте снова.");
                    continue;
            }
        }
    }
}