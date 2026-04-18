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
        if (course == null)
        {
            return false;
        }

        Enrollment enrollment = new Enrollment();
        enrollment.Student = this;
        enrollment.Course = course;

        Enrollments.Add(enrollment);

        return true;
    }

    public Submission SubmitAssignment(Assignment assignment, string filePath, string comment)
    {
        if (assignment == null)
        {
            return null;
        }

        Submission submission = new Submission();
        submission.Student = this;
        submission.Assignment = assignment;
        submission.FilePath = filePath;
        submission.Comment = comment;

        return submission;
    }

    public List<Grade> ViewGrades()
    {
        List<Grade> grades = new List<Grade>();
        grades.Add(new Grade());

        return grades;
    }
}