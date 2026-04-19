using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class EnrollmentTests
{
    [TestMethod]
    public void IsActive_DefaultEnrollment_ReturnsTrue()
    {
        Enrollment enrollment = new Enrollment();

        bool result = enrollment.IsActive();

        Assert.IsFalse(result);
    }
}