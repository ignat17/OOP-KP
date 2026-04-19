using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class AssignmentValidatorTests
{
    [TestMethod]
    public void ValidateAssignment_ValidAssignment_ReturnsTrue()
    {
        AssignmentValidator validator = new AssignmentValidator();
        Assignment assignment = new Assignment();

        assignment.Title = "Lab 1";
        assignment.MaxScore = 100;
        assignment.Deadline = DateTime.Now.AddDays(5);

        bool result = validator.ValidateAssignment(assignment);

        Assert.IsTrue(result);
    }
}