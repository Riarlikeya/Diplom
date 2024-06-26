namespace diplom;

public partial class Access
{
    public int Id { get; set; }

    public string Aname { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
