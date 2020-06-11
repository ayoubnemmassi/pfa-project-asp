using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication4.Models
{
    public partial class StudentContext : DbContext
    {
        public StudentContext()
        {
        }

        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Filiere> Filiere { get; set; }
        public virtual DbSet<Presence> Presence { get; set; }
        public virtual DbSet<Professor> Professor { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<StudentTable> StudentTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=DESKTOP-0FOH7DC;database=Student;integrated security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("admin");

                entity.Property(e => e.AdminId).HasColumnName("adminID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .IsRequired()
                    .HasColumnName("phonenumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('admin')");
            });

            modelBuilder.Entity<Filiere>(entity =>
            {
                entity.ToTable("filiere");

                entity.Property(e => e.FiliereId).HasColumnName("filiere_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Presence>(entity =>
            {
                entity.HasKey(e => new { e.SeanceId, e.StudentId, e.Date });

                entity.ToTable("presence");

                entity.Property(e => e.SeanceId).HasColumnName("seance_id");

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Absid)
                    .HasColumnName("absid")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(e => e.Profid);

                entity.ToTable("professor");

                entity.Property(e => e.Profid).HasColumnName("profid");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Classe).HasColumnName("classe");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .IsRequired()
                    .HasColumnName("phonenumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Speciality)
                    .IsRequired()
                    .HasColumnName("speciality")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('professor')");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("room");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.RoomName)
                    .IsRequired()
                    .HasColumnName("room_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SessionId)
                    .IsRequired()
                    .HasColumnName("session_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(e => new { e.SeanceId, e.FiliereId, e.Profid })
                    .HasName("PK__session__505A60152A166F67");

                entity.ToTable("session");

                entity.Property(e => e.SeanceId).HasColumnName("seance_id");

                entity.Property(e => e.FiliereId).HasColumnName("filiere_id");

                entity.Property(e => e.Profid).HasColumnName("profid");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Day)
                    .HasColumnName("day")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.HasOne(d => d.Filiere)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.FiliereId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__session__filiere__571DF1D5");

                entity.HasOne(d => d.Prof)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.Profid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__session__profid__5812160E");
            });

            modelBuilder.Entity<StudentTable>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.ToTable("student_table");

                entity.Property(e => e.StudentId).HasColumnName("studentID");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Approved).HasColumnName("approved");

                entity.Property(e => e.Classe).HasColumnName("classe");

                entity.Property(e => e.Filiere)
                    .HasColumnName("filiere")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .HasColumnName("mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .HasColumnName("phonenumber")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('student')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
