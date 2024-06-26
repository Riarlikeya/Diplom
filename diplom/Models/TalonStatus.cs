namespace diplom;

public partial class TalonStatus
{
    public int Id { get; set; }

    public string? Stitle { get; set; }

    public virtual ICollection<Talon> Talons { get; set; } = new List<Talon>();
}
