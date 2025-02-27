namespace Ebret4m4n.API.ChildBaseVaccines;

public class BaseVaccine
{
    public int id { get; set; }

    public string name { get; set; } = null!;

    public int docesRequired { get; set; }

    public int childAge { get; set; }
}
