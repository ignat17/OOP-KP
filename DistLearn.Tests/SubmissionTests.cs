using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class SubmissionTests
{
    [TestMethod]
    public void Send_ValidSubmission_ReturnsTrue()
    {
        Submission submission = new Submission();

        bool result = submission.Send();

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void UpdateComment_ValidComment_ReturnsTrue()
    {
        Submission submission = new Submission();

        bool result = submission.UpdateComment("New comment");

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsLate_LateSubmission_ReturnsTrue()
    {
        Submission submission = new Submission();

        bool result = submission.IsLate();

        Assert.IsTrue(result);
    }
}