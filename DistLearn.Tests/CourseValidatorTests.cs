using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class CourseValidatorTests
{
    [TestMethod]
    public void ValidateCourse_EmptyCourse_ReturnsTrue()
    {
        CourseValidator validator = new CourseValidator();
        Course course = new Course();

        bool result = validator.ValidateCourse(course);

        Assert.IsTrue(result);
    }
}