using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class MaterialTests
{
    [TestMethod]
    public void GetInfo_EmptyTitle_ReturnsEmptyString()
    {
        Material material = new Material();
        material.Title = "";

        string result = material.GetInfo();

        Assert.AreEqual("", result);
    }
}