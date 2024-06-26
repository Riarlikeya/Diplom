namespace diplom;

public partial class Gild
{
    public int Id { get; set; }

    public string Gname { get; set; } = null!;

    public string Gabbreviaton { get; set; } = null!;

    public virtual ICollection<Defect> Defects { get; set; } = new List<Defect>();
}
