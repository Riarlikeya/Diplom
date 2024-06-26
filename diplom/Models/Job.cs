namespace diplom;

public partial class Job
{
    public int Id { get; set; }

    public string Jname { get; set; } = null!;

    public string Jabbreviation { get; set; } = null!;

    public virtual ICollection<Defect> Defects { get; set; } = new List<Defect>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}