using System.Collections.Generic;

namespace DistLearn;

public class Course : IComparable<Course>
{
    public static int TotalCourses = 0;

    public string Title {get; set;}

    public string Description {get; set;}

    public Teacher Teacher {get; set;}

    public List<CourseContent> Contents {get; set;}

    public List<Enrollment> Enrollments {get; set;}

    public Course()
    {
        Title = "";
        Description = "";
        Teacher = null;
        Contents = new List<CourseContent>();
        Enrollments = new List<Enrollment>();

        TotalCourses++;
    }

    public bool AddContent(CourseContent content)
    {
        if (content == null)
        {
            return false;
        }

        Contents.Add(content);
        return true;
    }

    public bool RemoveContent(string title)
    {
        for (int i = 0; i < Contents.Count; i++)
        {
            if (Contents[i].Title == title)
            {
                Contents.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public int CompareTo(Course? other)
    {
        if (other == null)
        {
            return 1;
        }

        return Title.CompareTo(other.Title);
    }

    public int GetStudentsCount()
    {
        return Enrollments.Count;
    }
}