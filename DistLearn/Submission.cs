namespace DistLearn;

public class Submission
{
    public Student Student {get; set;}

    public Assignment Assignment {get; set;}

    public string FilePath {get; set;}

    public string Comment {get; set;}

    public DateTime SubmittedAt {get; set;}

    public string Status {get; set;}

    public Submission()
    {
        Student = null;
        Assignment = null;
        FilePath = "";
        Comment = "";
        SubmittedAt = DateTime.Now;
        Status = "Draft";
    }

    public bool Send()
    {
        if ((FilePath == null || FilePath.Trim() == "") &&
            (Comment == null || Comment.Trim() == ""))
        {
            return false;
        }

        Status = "Submitted";
        SubmittedAt = DateTime.Now;
        return true;
    }

    public bool UpdateComment(string newComment)
    {
        if (newComment == null || newComment.Trim() == "")
        {
            return false;
        }

        Comment = newComment;
        return true;
    }

    public bool IsLate()
    {
        if (Assignment == null)
        {
            return false;
        }

        if (SubmittedAt > Assignment.Deadline)
        {
            return true;
        }

        return false;
    }
}