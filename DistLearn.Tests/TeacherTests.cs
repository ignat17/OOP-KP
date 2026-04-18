using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class TeacherTests
{
    [TestMethod]
    public void CreateCourse_ValidData_ReturnsCourse()
    {
        Teacher teacher = new Teacher();

        Course result = teacher.CreateCourse("OOP", "Object Oriented Programming");

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void AddMaterial_ValidData_ReturnsTrue()
    {
        Teacher teacher = new Teacher();
        Course course = new Course();
        Material material = new Material();

        bool result = teacher.AddMaterial(course, material);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CreateAssignment_ValidData_ReturnsTrue()
    {
        Teacher teacher = new Teacher();
        Course course = new Course();
        Assignment assignment = new Assignment();

        bool result = teacher.CreateAssignment(course, assignment);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void GradeSubmission_ValidData_ReturnsTrue()
    {
        Teacher teacher = new Teacher();
        Submission submission = new Submission();
        Grade grade = new Grade();

        bool result = teacher.GradeSubmission(submission, grade);

        Assert.IsTrue(result);
    }
}