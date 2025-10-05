using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaGestionInventario.Models;

namespace SistemaGestionInventario.Data
{
    public partial class SistemaGestionInventarioContext : DbContext
    {
        public SistemaGestionInventarioContext (DbContextOptions<SistemaGestionInventarioContext> options)
            : base(options)
        {
        }

        public DbSet<SistemaGestionInventario.Models.ConfigEntityFramework> ConfigEntityFramework { get; set; } = default!;

        public virtual DbSet<Organization> Organizations { get; set; }

        public virtual DbSet<OrganizationUser> OrganizationUsers { get; set; }

        public virtual DbSet<OrganizationUserRole> OrganizationUserRoles { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_organizations");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CreateAt)
                    .HasDefaultValueSql("(switchoffset(sysdatetimeoffset(),'-04:00'))")
                    .HasColumnType("datetime");
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Status)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValue("AC");
            });

            modelBuilder.Entity<OrganizationUser>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.IdOrganization });

                entity.Property(e => e.IdOrganization).HasColumnName("idOrganization");
                entity.Property(e => e.IdUser).HasColumnName("idUser");
                entity.Property(e => e.JoinedAt)
                    .HasDefaultValueSql("(switchoffset(sysdatetimeoffset(),'-04:00'))")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Organization)
                    .WithMany(o => o.OrganizationUsers)
                    .HasForeignKey(d => d.IdOrganization)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk2_organization_users");

                entity.HasOne(d => d.User)
                    .WithMany(u => u.OrganizationUsers)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_organization_users");
            });

            modelBuilder.Entity<OrganizationUserRole>(entity =>
            {
                entity.HasKey(our => new { our.IdUser, our.IdOrganization, our.IdRole }); ;

                entity.Property(e => e.IdOrganization).HasColumnName("idOrganization");
                entity.Property(e => e.IdRole).HasColumnName("idRole");
                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.Organization)
                    .WithMany(r => r.OrganizationUserRole)
                    .HasForeignKey(d => d.IdOrganization)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk2_organization_user_roles");

                entity.HasOne(d => d.Role)
                    .WithMany(r => r.OrganizationUserRole)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk3_organization_user_roles");

                entity.HasOne(d => d.User)
                    .WithMany(u => u.OrganizationUserRole)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_organization_user_roles");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_roles");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.IdOrganization).HasColumnName("idOrganization");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Organization).WithMany(p => p.Roles)
                    .HasForeignKey(d => d.IdOrganization)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_roles_organizations");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_users");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Password).IsUnicode(false);
                entity.Property(e => e.Status)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasDefaultValue("AC");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC07770A02A5");

                entity.HasIndex(e => e.Key, "UQ__Permissi__C41E0289352771DD").IsUnique();

                entity.Property(e => e.Category).HasMaxLength(100);
                entity.Property(e => e.Key).HasMaxLength(100);
                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => new { e.IdRole, e.IdPermission });

                entity.Property(e => e.IdPermission).HasColumnName("idPermission");
                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.HasOne(d => d.Permission).WithMany()
                    .HasForeignKey(d => d.IdPermission)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Role).WithMany(r => r.RolePermissions)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
