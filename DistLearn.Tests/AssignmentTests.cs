using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class AssignmentTests
{
    [TestMethod]
    public void GetInfo_EmptyTitle_ReturnsAssignmentText()
    {
        Assignment assignment = new Assignment();
        assignment.Title = "";

        string result = assignment.GetInfo();

        Assert.AreEqual("Assignment: ", result);
    }

    [TestMethod]
    public void IsDeadlineExpired_DefaultAssignment_ReturnsFalse()
    {
        Assignment assignment = new Assignment();

        bool result = assignment.IsDeadlineExpired();

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ChangeDeadline_FutureDate_ReturnsTrue()
    {
        Assignment assignment = new Assignment();

        bool result = assignment.ChangeDeadline(DateTime.Now.AddDays(5));

        Assert.IsTrue(result);
    }
}