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
        submission.FilePath = "answer.txt";

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
        Assignment assignment = new Assignment();
        assignment.Deadline = DateTime.Now.AddDays(-1);

        Submission submission = new Submission();
        submission.Assignment = assignment;
        submission.SubmittedAt = DateTime.Now;

        bool result = submission.IsLate();

        Assert.IsTrue(result);
    }
}