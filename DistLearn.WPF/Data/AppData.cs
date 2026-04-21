using DistLearn;
using System;
using System.Collections.Generic;

namespace DistLearn.WPF.Data;

public static class AppData
{
    public static List<User> Users {get;} = new();
    public static List<Course> Courses {get;} = new();

    public static User CurrentUser {get; set;}

    public static void Initialize()
    {
        if (Users.Count > 0 || Courses.Count > 0)
        {
            return;
        }

        Administrator admin = new Administrator();
        admin.Login = "admin";
        admin.Password = "admin123";
        admin.FullName = "Адміністратор";

        Teacher teacher1 = new Teacher();
        teacher1.Login = "teacher1";
        teacher1.Password = "teacher123";
        teacher1.FullName = "Іваненко І. І.";
        teacher1.Department = "Програмування";

        Teacher teacher2 = new Teacher();
        teacher2.Login = "teacher2";
        teacher2.Password = "teacher123";
        teacher2.FullName = "Петренко О. П.";
        teacher2.Department = "Бази даних";

        Teacher teacher3 = new Teacher();
        teacher3.Login = "teacher3";
        teacher3.Password = "teacher123";
        teacher3.FullName = "Сидоренко М. В.";
        teacher3.Department = "Web";

        Teacher teacher4 = new Teacher();
        teacher4.Login = "teacher4";
        teacher4.Password = "teacher123";
        teacher4.FullName = "Коваленко Д. С.";
        teacher4.Department = "Алгоритми";

        Student student1 = new Student();
        student1.Login = "student1";
        student1.Password = "student123";
        student1.FullName = "Студент 1";
        student1.GroupName = "622п";

        Student student2 = new Student();
        student2.Login = "student2";
        student2.Password = "student123";
        student2.FullName = "Студент 2";
        student2.GroupName = "622п";

        Student student3 = new Student();
        student3.Login = "student3";
        student3.Password = "student123";
        student3.FullName = "Студент 3";
        student3.GroupName = "622п";

        Student student4 = new Student();
        student4.Login = "student4";
        student4.Password = "student123";
        student4.FullName = "Студент 4";
        student4.GroupName = "622п";

        Users.Add(admin);

        Users.Add(teacher1);
        Users.Add(teacher2);
        Users.Add(teacher3);
        Users.Add(teacher4);

        Users.Add(student1);
        Users.Add(student2);
        Users.Add(student3);
        Users.Add(student4);

        Course course1 = teacher1.CreateCourse(
            "Програмування на C#",
            "Курс присвячений вивченню основ мови програмування C#."
        );

        Course course2 = teacher2.CreateCourse(
            "Бази даних",
            "Основи реляційних баз даних і робота з SQL."
        );

        Course course3 = teacher3.CreateCourse(
            "Веб-розробка",
            "Основи HTML, CSS, JavaScript та створення веб-сторінок."
        );

        Course course4 = teacher4.CreateCourse(
            "Алгоритми та структури даних",
            "Базові алгоритми, сортування, пошук і структури даних."
        );

        AddCourseData(course1, "Лекція 1", "Вступ до C#", "Практична робота 1");
        AddCourseData(course2, "Лекція 1", "Вступ до баз даних", "Практична робота 1");
        AddCourseData(course3, "Лекція 1", "Вступ до веб-розробки", "Практична робота 1");
        AddCourseData(course4, "Лекція 1", "Вступ до алгоритмів", "Практична робота 1");

        EnrollStudent(student1, course1);
        EnrollStudent(student2, course1);
        EnrollStudent(student3, course1);

        EnrollStudent(student1, course2);
        EnrollStudent(student2, course2);

        EnrollStudent(student1, course3);
        EnrollStudent(student2, course3);
        EnrollStudent(student3, course3);
        EnrollStudent(student4, course3);

        EnrollStudent(student2, course4);
        EnrollStudent(student3, course4);
    }

    private static void AddCourseData(Course course, string materialTitle, string materialDesc, string assignmentTitle)
    {
        if (course == null)
        {
            return;
        }

        Material material = new Material();
        material.Title = materialTitle;
        material.Description = materialDesc;
        material.MaterialType = "Text";
        material.Url = "https://example.com";

        Assignment assignment = new Assignment();
        assignment.Title = assignmentTitle;
        assignment.Description = "Опис завдання";
        assignment.Instructions = "Виконати завдання та здати до дедлайну.";
        assignment.MaxScore = 100;
        assignment.Deadline = DateTime.Now.AddDays(7);

        course.AddContent(material);
        course.AddContent(assignment);

        Courses.Add(course);
    }

    private static void EnrollStudent(Student student, Course course)
    {
        if (student == null || course == null)
        {
            return;
        }

        student.EnrollToCourse(course);

        if (student.Enrollments.Count > 0)
        {
            Enrollment lastEnrollment = student.Enrollments[student.Enrollments.Count - 1];
            course.Enrollments.Add(lastEnrollment);
        }
    }
}