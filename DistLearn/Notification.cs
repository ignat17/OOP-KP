namespace DistLearn;

public class Notification
{
    public string Message {get; set;}

    public DateTime CreatedAt {get; set;}

    public bool IsRead {get; set;}

    public Notification()
    {
        Message = "";
        CreatedAt = DateTime.Now;
        IsRead = false;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}