namespace DistLearn;

public abstract class User
{
    public static int TotalUsers = 0;

    public string Login {get; set;}
    public string Password {get; set;}
    public string FullName {get; set;}
    public string Role {get; set;}

    public User()
    {
        Login = "";
        Password = "";
        FullName = "";
        Role = "";
        TotalUsers++;
    }

    public string GetRoleName()
    {
        return Role;
    }

    public bool ChangePassword(string newPassword)
    {
        return false;
    }
}