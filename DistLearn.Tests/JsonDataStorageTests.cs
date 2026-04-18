using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;

namespace DistLearn.Tests;

[TestClass]
public class JsonDataStorageTests
{
    [TestMethod]
    public void Save_ValidPath_ReturnsTrue()
    {
        JsonDataStorage storage = new JsonDataStorage();

        bool result = storage.Save("data.json", new object());

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Load_ValidPath_ReturnsObject()
    {
        JsonDataStorage storage = new JsonDataStorage();

        object result = storage.Load("data.json");

        Assert.IsNotNull(result);
    }
}