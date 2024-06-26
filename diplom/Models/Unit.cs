namespace diplom;

public partial class Unit
{
    public int Id { get; set; }

    public string Uname { get; set; } = null!;

    public string Uabbreviation { get; set; } = null!;

    public virtual ICollection<Defect> Defects { get; set; } = new List<Defect>();
}
