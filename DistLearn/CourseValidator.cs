namespace DistLearn;

public class CourseValidator
{
    public bool ValidateCourse(Course course)
    {
        if (course == null)
        {
            return false;
        }

        if (course.Title == null || course.Title.Trim() == "")
        {
            return false;
        }

        if (course.Teacher == null)
        {
            return false;
        }

        return true;
    }
}