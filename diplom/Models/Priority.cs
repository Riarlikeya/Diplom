namespace diplom;

public partial class Priority
{
    public int Id { get; set; }

    public string Pname { get; set; } = null!;

    public virtual ICollection<Defect> Defects { get; set; } = new List<Defect>();
}
