namespace diplom;

public partial class SysRole
{
    public int Id { get; set; }

    public string Rname { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
