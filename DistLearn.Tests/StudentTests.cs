using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void EnrollToCourse_ValidCourse_ReturnsTrue()
    {
        Student student = new Student();

        Course course = new Course();

        bool result = student.EnrollToCourse(course);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void SubmitAssignment_ValidData_ReturnsSubmission()
    {
        Student student = new Student();

        Assignment assignment = new Assignment();

        Submission result = student.SubmitAssignment(assignment, "file.txt", "My answer");

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void ViewGrades_ReturnsListWithGrades()
    {
        Student student = new Student();

        List<Grade> result = student.ViewGrades();

        Assert.IsTrue(result.Count > 0);
    }
}