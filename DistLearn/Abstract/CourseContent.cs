namespace DistLearn;

public abstract class CourseContent
{
    public string Title {get; set;}
    public string Description {get; set;}
    public DateTime CreatedAt {get; set;}

    public CourseContent()
    {
        Title = "";
        Description = "";
        CreatedAt = DateTime.Now;
    }

    public abstract string GetInfo();
}