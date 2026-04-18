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
        return false; //-заглушка
    }

    public bool UpdateComment(string newComment)
    {
        return false; //-заглушка
    }

    public bool IsLate()
    {
        return false; //-заглушка
    }
}