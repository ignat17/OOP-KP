namespace DistLearn;

public class Enrollment
{
    public Student Student {get; set;}

    public Course Course {get; set;}

    public DateTime EnrolledAt {get; set;}

    public Enrollment()
    {
        Student = null;
        Course = null;
        EnrolledAt = DateTime.Now;
    }

    public bool IsActive()
    {
        if (Student == null || Course == null)
        {
            return false;
        }

        return true;
    }
}