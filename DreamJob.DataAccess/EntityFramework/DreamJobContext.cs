using System;
using System.Collections.Generic;
using DreamJob.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DreamJob.DataAccess.EntityFramework;

public partial class DreamJobContext : DbContext
{
    public DreamJobContext()
    {
    }

    public DreamJobContext(DbContextOptions<DreamJobContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<CandidateSkill> CandidateSkills { get; set; }

    public virtual DbSet<CandidatesCareerField> CandidatesCareerFields { get; set; }

    public virtual DbSet<CareerField> CareerFields { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<EmployersCareerField> EmployersCareerFields { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<Interaction> Interactions { get; set; }

    public virtual DbSet<JobOffer> JobOffers { get; set; }

    public virtual DbSet<JobSkill> JobSkills { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Study> Studies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => /*optionsBuilder.UseSqlServer("Server=DESKTOP-9E5HMJQ\\SQL_SERVER;Initial Catalog=DreamJob;Integrated Security=true;\nTrustServerCertificate= true;");*/
        optionsBuilder.UseSqlServer("Server=tcp:dreamjob.database.windows.net,1433;Initial Catalog=DreamJob;Persist Security Info=False;User ID=stefan;Password=ADMINPA55!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Candidates_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Linkedin)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Surname).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Candidates_UserId_FK");
        });

        modelBuilder.Entity<CandidateSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CandidateSkill_PK");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Candidate).WithMany(p => p.CandidateSkills)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("CandidateSkills_CandidateId_FK");

            entity.HasOne(d => d.Skill).WithMany(p => p.CandidateSkills)
                .HasForeignKey(d => d.SkillId)
                .HasConstraintName("CandidateSkills_SkillId_FK");
        });

        modelBuilder.Entity<CandidatesCareerField>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CareerFieldsCandidates_PK");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Candidate).WithMany(p => p.CandidatesCareerFields)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("CareerFieldsCandidates_CandidateId_FK");

            entity.HasOne(d => d.CareerField).WithMany(p => p.CandidatesCareerFields)
                .HasForeignKey(d => d.CareerFieldId)
                .HasConstraintName("CareerFieldsCandidates_CareerFieldId");
        });

        modelBuilder.Entity<CareerField>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CareerFields_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CareerFieldName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Emlpoyer_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployerLinkedin)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmployerName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OfficeLocation)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Employers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Employers_UserId_FK");
        });

        modelBuilder.Entity<EmployersCareerField>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmployersCareerFields_PK");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.CareerField).WithMany(p => p.EmployersCareerFields)
                .HasForeignKey(d => d.CareerFieldId)
                .HasConstraintName("EmployersCareerFields_CareerFieldId");

            entity.HasOne(d => d.Employer).WithMany(p => p.EmployersCareerFields)
                .HasForeignKey(d => d.EmployerId)
                .HasConstraintName("EmployersCareerFields_EmployerId_FK");
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Experiences_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EndYear).HasColumnType("date");
            entity.Property(e => e.ExperienceName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StartYear).HasColumnType("date");

            entity.HasOne(d => d.Candidate).WithMany(p => p.Experiences)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("Experiences_CandidateId_FK");
        });

        modelBuilder.Entity<Interaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Interactions_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.InteractionDate).HasColumnType("date");

            entity.HasOne(d => d.Candidate).WithMany(p => p.Interactions)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("Interactions_CandidateId_FK");

            entity.HasOne(d => d.JobOffer).WithMany(p => p.Interactions)
                .HasForeignKey(d => d.JobOfferId)
                .HasConstraintName("Interactions_JobOfferId_FK");
        });

        modelBuilder.Entity<JobOffer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("JobOffer_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Employer).WithMany(p => p.JobOffers)
                .HasForeignKey(d => d.EmployerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("JobOffer_EmployerId_FK");
        });

        modelBuilder.Entity<JobSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("JobSkills_PK");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.JobOffer).WithMany(p => p.JobSkills)
                .HasForeignKey(d => d.JobOfferId)
                .HasConstraintName("FK__JobSkills__JobOf__7E37BEF6");

            entity.HasOne(d => d.Skill).WithMany(p => p.JobSkills)
                .HasForeignKey(d => d.SkillId)
                .HasConstraintName("FK__JobSkills__Skill__7F2BE32F");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Roles_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Skills_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SkillName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Study>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Studies_PK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EndYear).HasColumnType("date");
            entity.Property(e => e.Specialty)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StartYear).HasColumnType("date");
            entity.Property(e => e.University)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Candidate).WithMany(p => p.Studies)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("Studies_CandidateId_FK");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_PK");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E403C49CF7").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534FCD00689").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.UserPassword).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("Users_RoleId_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
