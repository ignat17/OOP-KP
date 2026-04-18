namespace DistLearn;

public class Material : CourseContent
{
    public string FilePath {get; set;}

    public string Url {get; set;}

    public string MaterialType {get; set;}

    public Material()
    {
        FilePath = "";
        Url = "";
        MaterialType = "";
    }

    public override string GetInfo()
    {
        return "Material: " + Title;
    }
}