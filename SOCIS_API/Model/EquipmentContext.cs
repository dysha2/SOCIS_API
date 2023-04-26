using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SOCIS_API.Model
{
    public partial class EquipmentContext : DbContext
    {
        public EquipmentContext()
        {
        }

        public EquipmentContext(DbContextOptions<EquipmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountingUnit> AccountingUnits { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Firm> Firms { get; set; } = null!;
        public virtual DbSet<FullNameUnit> FullNameUnits { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Place> Places { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestUnit> RequestUnits { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ShortTermMove> ShortTermMoves { get; set; } = null!;
        public virtual DbSet<UnitPlace> UnitPlaces { get; set; } = null!;
        public virtual DbSet<UnitRespPerson> UnitRespPeople { get; set; } = null!;
        public virtual DbSet<UnitType> UnitTypes { get; set; } = null!;
        public virtual DbSet<WorkOnRequest> WorkOnRequests { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:default");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountingUnit>(entity =>
            {
                entity.Property(e => e.Mac).IsFixedLength();

                entity.HasOne(d => d.FullNameUnit)
                    .WithMany(p => p.AccountingUnits)
                    .HasForeignKey(d => d.FullNameUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountingUnit_AbstractDevice");
            });

            modelBuilder.Entity<FullNameUnit>(entity =>
            {
                entity.HasOne(d => d.Firm)
                    .WithMany(p => p.FullNameUnits)
                    .HasForeignKey(d => d.FirmId)
                    .HasConstraintName("FK_FullNameUnit_Firm");

                entity.HasOne(d => d.UnitType)
                    .WithMany(p => p.FullNameUnits)
                    .HasForeignKey(d => d.UnitTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FullNameUnit_UnitType");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasIndex(e => e.UserName, "UniqueUsernameIndx")
                    .IsUnique()
                    .HasFilter("([UserName] IS NOT NULL)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Person_Department");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Person_Post");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Person_Role");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.DateTimeStart).HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.Declarant)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.DeclarantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Person");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Place");
            });

            modelBuilder.Entity<RequestUnit>(entity =>
            {
                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.RequestUnits)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestUnits_AccountingUnit");

                entity.HasOne(d => d.WorkOnRequest)
                    .WithMany(p => p.RequestUnits)
                    .HasForeignKey(d => d.WorkOnRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestUnits_WorkOnRequest");
            });

            modelBuilder.Entity<ShortTermMove>(entity =>
            {
                entity.HasOne(d => d.Place)
                    .WithMany(p => p.ShortTermMoves)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShortTermMove_Place");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.ShortTermMoves)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShortTermMove_AccountingUnit");

                entity.HasOne(d => d.WorkOnRequest)
                    .WithMany(p => p.ShortTermMoves)
                    .HasForeignKey(d => d.WorkOnRequestId)
                    .HasConstraintName("FK_ShortTermMove_WorkOnRequest");
            });

            modelBuilder.Entity<UnitPlace>(entity =>
            {
                entity.HasOne(d => d.Place)
                    .WithMany(p => p.UnitPlaces)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnitPlace_Place");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.UnitPlaces)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnitPlace_AccountingUnit");

                entity.HasOne(d => d.WorkOnRequest)
                    .WithMany(p => p.UnitPlaces)
                    .HasForeignKey(d => d.WorkOnRequestId)
                    .HasConstraintName("FK_UnitPlace_WorkOnRequest");
            });

            modelBuilder.Entity<UnitRespPerson>(entity =>
            {
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.UnitRespPeople)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnitRespPerson_ResponsiblePerson");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.UnitRespPeople)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnitRespPerson_AccountingUnit");
            });

            modelBuilder.Entity<WorkOnRequest>(entity =>
            {
                entity.Property(e => e.DateTime).HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.Implementer)
                    .WithMany(p => p.WorkOnRequests)
                    .HasForeignKey(d => d.ImplementerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOnRequest_Person");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.WorkOnRequests)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOnRequest_Request");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.WorkOnRequests)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOnRequest_Service");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
