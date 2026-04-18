using System.Collections.Generic;

namespace DistLearn;

public class Teacher : User
{
    public string Department {get; set;}

    public List<Course> Courses {get; set;}

    public Teacher()
    {
        Role = "Teacher";
        Department = "";
        Courses = new List<Course>();
    }

    public Course CreateCourse(string title, string description)
    {
        return null; //-заглушка
    }

    public bool AddMaterial(Course course, Material material)
    {
        return false; //-заглушка
    }

    public bool CreateAssignment(Course course, Assignment assignment)
    {
        return false; //-заглушка
    }

    public bool GradeSubmission(Submission submission, Grade grade)
    {
        return false; //-заглушка
    }
}