using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class UserValidatorTests
{
    [TestMethod]
    public void ValidateUser_EmptyStudent_ReturnsTrue()
    {
        UserValidator validator = new UserValidator();
        Student student = new Student();

        bool result = validator.ValidateUser(student);

        Assert.IsTrue(result);
    }
}