using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class GradeTests
{
    [TestMethod]
    public void SetScore_ValidScore_ReturnsTrue()
    {
        Grade grade = new Grade();

        bool result = grade.SetScore(90);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void AddComment_ValidComment_CommentChanges()
    {
        Grade grade = new Grade();

        grade.AddComment("Good work");

        Assert.AreEqual("Good work", grade.TeacherComment);
    }
}