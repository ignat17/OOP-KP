using System.IO;
using System.Text.Json;

namespace DistLearn;

public class JsonDataStorage : IDataStorage
{
    public bool Save(string filePath, object data)
    {
        if (filePath == null || filePath.Trim() == "")
        {
            return false;
        }

        string json = JsonSerializer.Serialize(data);
        File.WriteAllText(filePath, json);

        return true;
    }

    public object Load(string filePath)
    {
        if (filePath == null || filePath.Trim() == "")
        {
            return null;
        }

        if (!File.Exists(filePath))
        {
            return null;
        }

        return File.ReadAllText(filePath);
    }
}