using Microsoft.EntityFrameworkCore;

namespace diplom;

public partial class RgresDbContext : DbContext
{
    public RgresDbContext()
    {
    }

    public RgresDbContext(DbContextOptions<RgresDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Access> Accesses { get; set; }

    public virtual DbSet<DefStatus> DefStatuses { get; set; }

    public virtual DbSet<Defect> Defects { get; set; }

    public virtual DbSet<Gild> Gilds { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Priority> Priorities { get; set; }

    public virtual DbSet<SysRole> SysRoles { get; set; }

    public virtual DbSet<Talon> Talons { get; set; }

    public virtual DbSet<TalonStatus> TalonStatuses { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=rgres-db;Username=postgres;Password=root");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Access>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("access_pkey");

            entity.ToTable("access");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Aname).HasColumnName("aname");
        });

        modelBuilder.Entity<DefStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("def_statuses_pkey");

            entity.ToTable("def_statuses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sname).HasColumnName("sname");
        });

        modelBuilder.Entity<Defect>(entity =>
        {


            entity.HasKey(e => e.Id).HasName("defects_pkey");

            entity.ToTable("defects");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorMes).HasColumnName("author_mes");
            entity.Property(e => e.AuthorReg).HasColumnName("author_reg");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Enddate)
                .HasColumnType("date")
                .HasColumnName("enddate");
            entity.Property(e => e.Enddcomment).HasColumnName("enddcomment");
            entity.Property(e => e.Factdate)
                .HasColumnType("date")
                .HasColumnName("factdate");
            entity.Property(e => e.FioTa).HasColumnName("fio_ta");
            entity.Property(e => e.FullDesc).HasColumnName("full_desc");
            entity.Property(e => e.Gildid).HasColumnName("gildid");
            entity.Property(e => e.Planneddate)
                .HasColumnType("date")
                .HasColumnName("planneddate");
            entity.Property(e => e.Prioretyid).HasColumnName("prioretyid");
            entity.Property(e => e.ShortDesc)
                .HasMaxLength(100)
                .HasColumnName("short_desc");
            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Unitid).HasColumnName("unitid");
            entity.Property(e => e.Urid).HasColumnName("urid");

            entity.HasOne(d => d.AuthorMesNavigation).WithMany(p => p.DefectAuthorMesNavigations)
                .HasForeignKey(d => d.AuthorMes)
                .HasConstraintName("defects_author_mes_fkey");

            entity.HasOne(d => d.AuthorRegNavigation).WithMany(p => p.DefectAuthorRegNavigations)
                .HasForeignKey(d => d.AuthorReg)
                .HasConstraintName("defects_author_reg_fkey").OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.FioTaNavigation).WithMany(p => p.DefectFioTaNavigations)
                .HasForeignKey(d => d.FioTa)
                .HasConstraintName("defects_fio_ta_fkey");

            entity.HasOne(d => d.Gild).WithMany(p => p.Defects)
                .HasForeignKey(d => d.Gildid)
                .HasConstraintName("defects_gildid_fkey");

            entity.HasOne(d => d.Priorety).WithMany(p => p.Defects)
                .HasForeignKey(d => d.Prioretyid)
                .HasConstraintName("defects_prioretyid_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Defects)
                .HasForeignKey(d => d.Statusid)
                .HasConstraintName("defects_statusid_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.Defects)
                .HasForeignKey(d => d.Unitid)
                .HasConstraintName("defects_unitid_fkey");

            entity.HasOne(d => d.Ur).WithMany(p => p.Defects)
                .HasForeignKey(d => d.Urid)
                .HasConstraintName("defects_urid_fkey");
        });

        modelBuilder.Entity<Gild>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gilds_pkey");

            entity.ToTable("gilds");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Gabbreviaton).HasColumnName("gabbreviaton");
            entity.Property(e => e.Gname).HasColumnName("gname");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jobs_pkey");

            entity.ToTable("jobs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Jabbreviation).HasColumnName("jabbreviation");
            entity.Property(e => e.Jname).HasColumnName("jname");
        });

        modelBuilder.Entity<Priority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("priority_pkey");

            entity.ToTable("priority");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Pname).HasColumnName("pname");
        });

        modelBuilder.Entity<SysRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sys_roles_pkey");

            entity.ToTable("sys_roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Rname).HasColumnName("rname");
        });

        modelBuilder.Entity<Talon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("talons_pkey");

            entity.ToTable("talons");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DefectId).HasColumnName("defect_id");
            entity.Property(e => e.Emaildop).HasColumnName("emaildop");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Readytime).HasColumnName("readytime");
            entity.Property(e => e.Regemail).HasColumnName("regemail");
            entity.Property(e => e.Reguser).HasColumnName("reguser");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.TstatusId).HasColumnName("tstatus_id");

            entity.HasOne(d => d.Defect).WithMany(p => p.Talons)
                .HasForeignKey(d => d.DefectId)
                .HasConstraintName("talons_defect_id_fkey");

            entity.HasOne(d => d.ReguserNavigation).WithMany(p => p.Talons)
                .HasForeignKey(d => d.Reguser)
                .HasConstraintName("talons_reguser_fkey").OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Tstatus).WithMany(p => p.Talons)
                .HasForeignKey(d => d.TstatusId)
                .HasConstraintName("talons_tstatus_id_fkey");
        });

        modelBuilder.Entity<TalonStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("talon_statuses_pkey");

            entity.ToTable("talon_statuses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Stitle).HasColumnName("stitle");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("units_pkey");

            entity.ToTable("units");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Uabbreviation).HasColumnName("uabbreviation");
            entity.Property(e => e.Uname).HasColumnName("uname");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accessid).HasColumnName("accessid");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Fio).HasColumnName("fio");
            entity.Property(e => e.Jobid).HasColumnName("jobid");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Pnumber).HasColumnName("pnumber");
            entity.Property(e => e.Sroleid).HasColumnName("sroleid");

            entity.HasOne(d => d.Access).WithMany(p => p.Users)
                .HasForeignKey(d => d.Accessid)
                .HasConstraintName("users_accessid_fkey");

            entity.HasOne(d => d.Job).WithMany(p => p.Users)
                .HasForeignKey(d => d.Jobid)
                .HasConstraintName("users_jobid_fkey");

            entity.HasOne(d => d.Srole).WithMany(p => p.Users)
                .HasForeignKey(d => d.Sroleid)
                .HasConstraintName("users_sroleid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}