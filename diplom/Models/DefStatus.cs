namespace diplom;

public partial class DefStatus
{
    public int Id { get; set; }

    public string Sname { get; set; } = null!;

    public virtual ICollection<Defect> Defects { get; set; } = new List<Defect>();
}
