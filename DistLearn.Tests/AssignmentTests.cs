using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class AssignmentTests
{
    [TestMethod]
    public void GetInfo_EmptyTitle_ReturnsEmptyString()
    {
        Assignment assignment = new Assignment();
        assignment.Title = "";

        string result = assignment.GetInfo();

        Assert.AreEqual("", result);
    }

    [TestMethod]
    public void IsDeadlineExpired_DefaultAssignment_ReturnsTrue()
    {
        Assignment assignment = new Assignment();

        bool result = assignment.IsDeadlineExpired();

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ChangeDeadline_PastDate_ReturnsTrue()
    {
        Assignment assignment = new Assignment();

        bool result = assignment.ChangeDeadline(DateTime.Now.AddDays(-1));

        Assert.IsTrue(result);
    }
}