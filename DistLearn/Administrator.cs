namespace DistLearn;

public class Administrator : User
{
    public int AccessLevel {get; set;}

    public Administrator()
    {
        Role = "Administrator";
        AccessLevel = 1;
    }

    public bool AddUser(User user)
    {
        return false; //-заглушка
    }

    public bool RemoveUser(string login)
    {
        return false; //-заглушка
    }

    public bool SaveData(string filePath)
    {
        return false; //-заглушка
    }

    public bool LoadData(string filePath)
    {
        return false; //-заглушка
    }
}