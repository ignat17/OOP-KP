namespace DistLearn;

public interface IDataStorage
{
    bool Save(string filePath, object data);

    object Load(string filePath);
}