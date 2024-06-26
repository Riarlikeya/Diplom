namespace diplom;
public partial class Talon
{
    public int Id { get; set; }

    public int? DefectId { get; set; }

    public int? Reguser { get; set; }

    public string? Regemail { get; set; }

    public string Readytime { get; set; } = null!;

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }

    public string? Emaildop { get; set; }

    public int? TstatusId { get; set; }

    public virtual Defect? Defect { get; set; }

    public virtual User? ReguserNavigation { get; set; }

    public virtual TalonStatus? Tstatus { get; set; }
}

