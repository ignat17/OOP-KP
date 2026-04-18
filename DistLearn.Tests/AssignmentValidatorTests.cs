using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class AssignmentValidatorTests
{
    [TestMethod]
    public void ValidateAssignment_EmptyAssignment_ReturnsTrue()
    {
        AssignmentValidator validator = new AssignmentValidator();
        Assignment assignment = new Assignment();

        bool result = validator.ValidateAssignment(assignment);

        Assert.IsTrue(result);
    }
}