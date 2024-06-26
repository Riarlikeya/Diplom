namespace diplom;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fio { get; set; } = null!;

    public string Pnumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? Sroleid { get; set; }

    public int? Jobid { get; set; }

    public int? Accessid { get; set; }

    public virtual Access? Access { get; set; }

    public virtual ICollection<Defect> DefectAuthorMesNavigations { get; set; } = new List<Defect>();

    public virtual ICollection<Defect> DefectAuthorRegNavigations { get; set; } = new List<Defect>();

    public virtual ICollection<Defect> DefectFioTaNavigations { get; set; } = new List<Defect>();

    public virtual Job? Job { get; set; }

    public virtual SysRole? Srole { get; set; }

    public virtual ICollection<Talon> Talons { get; set; } = new List<Talon>();
}
