namespace diplom;

public partial class Defect
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int? Gildid { get; set; }

    public int? Unitid { get; set; }

    public string ShortDesc { get; set; } = null!;

    public string? FullDesc { get; set; }

    public int? AuthorReg { get; set; }

    public int? AuthorMes { get; set; }

    public int? FioTa { get; set; }

    public int? Prioretyid { get; set; }

    public int? Statusid { get; set; }

    public DateOnly? Planneddate { get; set; }

    public DateOnly? Factdate { get; set; }

    public string? Comment { get; set; }

    public DateOnly? Enddate { get; set; }

    public string? Enddcomment { get; set; }

    public int? Urid { get; set; }

    public virtual User? AuthorMesNavigation { get; set; }

    public virtual User? AuthorRegNavigation { get; set; }

    public virtual User? FioTaNavigation { get; set; }

    public virtual Gild? Gild { get; set; }

    public virtual Priority? Priorety { get; set; }

    public virtual DefStatus? Status { get; set; }

    public virtual ICollection<Talon> Talons { get; set; } = new List<Talon>();

    public virtual Unit? Unit { get; set; }

    public virtual Job? Ur { get; set; }
}
