namespace DistLearn;

public class Grade
{
    public int Score {get; set;}

    public string TeacherComment {get; set;}

    public DateTime GradedAt {get; set;}

    public Grade()
    {
        Score = 0;
        TeacherComment = "";
        GradedAt = DateTime.Now;
    }

    public bool SetScore(int value)
    {
        if (value < 0)
        {
            return false;
        }

        Score = value;
        GradedAt = DateTime.Now;
        return true;
    }

    public void AddComment(string comment)
    {
        if (comment == null)
        {
            return;
        }

        TeacherComment = comment;
    }
}