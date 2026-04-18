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
        if (title == null || title.Trim() == "")
        {
            return null;
        }

        Course course = new Course();
        course.Title = title;
        course.Description = description;
        course.Teacher = this;

        Courses.Add(course);

        return course;
    }

    public bool AddMaterial(Course course, Material material)
    {
        if (course == null || material == null)
        {
            return false;
        }

        course.Contents.Add(material);
        return true;
    }

    public bool CreateAssignment(Course course, Assignment assignment)
    {
        if (course == null || assignment == null)
        {
            return false;
        }

        course.Contents.Add(assignment);
        return true;
    }

    public bool GradeSubmission(Submission submission, Grade grade)
    {
        if (submission == null || grade == null)
        {
            return false;
        }

        return true;
    }
}