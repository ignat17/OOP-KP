namespace DistLearn;

public class JsonDataStorage : IDataStorage
{
    public bool Save(string filePath, object data)
    {
        return false; //-заглушка
    }

    public object Load(string filePath)
    {
        return null; //-заглушка
    }
}