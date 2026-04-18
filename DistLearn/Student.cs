using System.Collections.Generic;

namespace DistLearn;

public class Student : User
{
    public string GroupName {get; set;}

    public List<Enrollment> Enrollments {get; set;}

    public Student()
    {
        Role = "Student";
        GroupName = "";
        Enrollments = new List<Enrollment>();
    }

    public bool EnrollToCourse(Course course)
    {
        return false; //-заглушка
    }

    public Submission SubmitAssignment(Assignment assignment, string filePath, string comment)
    {
        return null; //-заглушка
    }

    public List<Grade> ViewGrades()
    {
        return new List<Grade>(); //-заглушка
    }
}