using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class CourseValidatorTests
{
    [TestMethod]
    public void ValidateCourse_ValidCourse_ReturnsTrue()
    {
        CourseValidator validator = new CourseValidator();
        Course course = new Course();
        Teacher teacher = new Teacher();

        course.Title = "OOP";
        course.Teacher = teacher;

        bool result = validator.ValidateCourse(course);

        Assert.IsTrue(result);
    }
}