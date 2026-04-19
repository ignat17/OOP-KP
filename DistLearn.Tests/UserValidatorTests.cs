using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class UserValidatorTests
{
    [TestMethod]
    public void ValidateUser_ValidStudent_ReturnsTrue()
    {
        UserValidator validator = new UserValidator();
        Student student = new Student();

        student.Login = "student1";
        student.Password = "12345678";
        student.FullName = "Ivan Ivanov";
        student.Role = "Student";

        bool result = validator.ValidateUser(student);

        Assert.IsTrue(result);
    }
}