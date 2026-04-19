using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class NotificationTests
{
    [TestMethod]
    public void MarkAsRead_AfterCall_IsTrue()
    {
        Notification notification = new Notification();

        notification.MarkAsRead();

        Assert.IsTrue(notification.IsRead);
    }
}