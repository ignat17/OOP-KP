namespace DistLearn;

public class UserValidator
{
    public bool ValidateUser(User user)
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

        if (user.Role == null || user.Role.Trim() == "")
        {
            return false;
        }

        return true;
    }
}