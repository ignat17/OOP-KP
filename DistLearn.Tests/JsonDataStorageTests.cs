using Microsoft.VisualStudio.TestTools.UnitTesting;
using DistLearn;
using System.IO;

namespace DistLearn.Tests;

[TestClass]
public class JsonDataStorageTests
{
    [TestMethod]
    public void Save_ValidPath_ReturnsTrue()
    {
        JsonDataStorage storage = new JsonDataStorage();
        string filePath = "save_test.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        bool result = storage.Save(filePath, new {Name = "Test"});

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Load_ValidPath_ReturnsObject()
    {
        JsonDataStorage storage = new JsonDataStorage();
        string filePath = "load_test.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        storage.Save(filePath, new {Name = "Test"});
        object result = storage.Load(filePath);

        Assert.IsNotNull(result);
    }
}