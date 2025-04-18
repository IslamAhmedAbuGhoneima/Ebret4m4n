namespace Ebret4m4n.API.ChildBaseVaccines;

public class BaseVaccine
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    public int childAge { get; set; }

    public List<string> antigens { get; set; } = [];
}
