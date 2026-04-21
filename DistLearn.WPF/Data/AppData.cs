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
            return;

        var admin = new Administrator
        {
            Login = "admin",
            Password = "admin123",
            FullName = "Адміністратор"
        };

        var teacher = new Teacher
        {
            Login = "teacher",
            Password = "teacher123",
            FullName = "Викладач",
            Department = "Програмування"
        };

        var student = new Student
        {
            Login = "student",
            Password = "student123",
            FullName = "Студент",
            GroupName = "622п"
        };

        Users.Add(admin);
        Users.Add(teacher);
        Users.Add(student);

        var course = teacher.CreateCourse(
            "Об'єктно-орієнтоване програмування",
            "Основи ООП, класи, об'єкти, спадкування."
        );

        if (course != null)
        {
            var material = new Material
            {
                Title = "Лекція 1",
                Description = "Вступ до ООП",
                MaterialType = "Text",
                Url = "https://example.com"
            };

            var assignment = new Assignment
            {
                Title = "Практична робота 1",
                Description = "Створити декілька класів",
                Instructions = "Реалізувати просту модель предметної області",
                MaxScore = 100,
                Deadline = DateTime.Now.AddDays(7)
            };

            course.AddContent(material);
            course.AddContent(assignment);

            Courses.Add(course);
        }
    }
}