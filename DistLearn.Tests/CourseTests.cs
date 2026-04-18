using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class CourseTests
{
    [TestMethod]
    public void AddContent_EmptyTitle_ReturnsFalse()
    {
        Course course = new Course();
        Material material = new Material();
        material.Title = "";

        bool result = course.AddContent(material);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void RemoveContent_UnknownTitle_ReturnsTrue()
    {
        Course course = new Course();

        bool result = course.RemoveContent("Unknown");

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void GetStudentsCount_EmptyList_ReturnsMoreThanZero()
    {
        Course course = new Course();

        int result = course.GetStudentsCount();

        Assert.IsTrue(result > 0);
    }
}