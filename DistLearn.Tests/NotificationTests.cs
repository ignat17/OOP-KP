using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class NotificationTests
{
    [TestMethod]
    public void MarkAsRead_AfterCall_IsStillFalse()
    {
        Notification notification = new Notification();

        notification.MarkAsRead();

        Assert.IsFalse(notification.IsRead);
    }
}