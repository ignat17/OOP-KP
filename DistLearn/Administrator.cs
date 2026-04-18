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
        if (user == null)
        {
            return false;
        }

        if (user.Login == null || user.Login.Trim() == "")
        {
            return false;
        }

        if (user.Password == null || user.Password.Length < 8)
        {
            return false;
        }

        if (user.FullName == null || user.FullName.Trim() == "")
        {
            return false;
        }

        return true;
    }

    public bool RemoveUser(string login)
    {
        if (login == null || login.Trim() == "")
        {
            return false;
        }

        return true;
    }

    public bool SaveData(string filePath)
    {
        if (filePath == null || filePath.Trim() == "")
        {
            return false;
        }

        return true;
    }

    public bool LoadData(string filePath)
    {
        if (filePath == null || filePath.Trim() == "")
        {
            return false;
        }

        return true;
    }
}