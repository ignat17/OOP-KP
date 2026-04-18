namespace DistLearn;

public class Assignment : CourseContent
{
    public DateTime Deadline {get; set;}

    public int MaxScore {get; set;}

    public string Instructions {get; set;}

    public Assignment()
    {
        Deadline = DateTime.Now.AddDays(7);
        MaxScore = 100;
        Instructions = "";
    }

    public override string GetInfo()
    {
        return "Assignment: " + Title;
    }

    public bool IsDeadlineExpired()
    {
        if (DateTime.Now > Deadline)
        {
            return true;
        }

        return false;
    }

    public bool ChangeDeadline(DateTime newDate)
    {
        if (newDate <= DateTime.Now)
        {
            return false;
        }

        Deadline = newDate;
        return true;
    }
}