namespace DistLearn;

public class AssignmentValidator
{
    public bool ValidateAssignment(Assignment assignment)
    {
        if (assignment == null)
        {
            return false;
        }

        if (assignment.Title == null || assignment.Title.Trim() == "")
        {
            return false;
        }

        if (assignment.MaxScore <= 0)
        {
            return false;
        }

        if (assignment.Deadline <= DateTime.Now)
        {
            return false;
        }

        return true;
    }
}