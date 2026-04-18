using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class AdministratorTests
{
    [TestMethod]
    public void AddUser_ValidUser_ReturnsTrue()
    {
        Administrator admin = new Administrator();
        Student student = new Student();

        student.Login = "student1";
        student.Password = "12345678";
        student.FullName = "Ivan Ivanov";

        bool result = admin.AddUser(student);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void RemoveUser_ExistingLogin_ReturnsTrue()
    {
        Administrator admin = new Administrator();
        string login = "student1";

        bool result = admin.RemoveUser(login);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void SaveData_ValidPath_ReturnsTrue()
    {
        Administrator admin = new Administrator();
        string filePath = "data.json";

        bool result = admin.SaveData(filePath);

        Assert.IsTrue(result);
    }
}