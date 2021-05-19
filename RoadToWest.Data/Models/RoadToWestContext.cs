using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RoadToWest.Data.Models
{
    public partial class RoadToWestContext : DbContext
    {
        public RoadToWestContext()
        {
        }

        public RoadToWestContext(DbContextOptions<RoadToWestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<OrderTool> OrderTool { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Scene> Scene { get; set; }
        public virtual DbSet<SceneCharacter> SceneCharacter { get; set; }
        public virtual DbSet<Tool> Tool { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=RoadToWest;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Actor)
                    .HasForeignKey<Actor>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actor_User");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Author).HasMaxLength(50);

                entity.Property(e => e.Modified).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_History_User");
            });

            modelBuilder.Entity<OrderTool>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Author).HasMaxLength(50);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.SceneId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TimeEnd).HasColumnType("datetime");

                entity.Property(e => e.TimeStart).HasColumnType("datetime");

                entity.Property(e => e.ToolId).HasMaxLength(50);

                entity.HasOne(d => d.Scene)
                    .WithMany(p => p.OrderTool)
                    .HasForeignKey(d => d.SceneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderTool_Scene");

                entity.HasOne(d => d.Tool)
                    .WithMany(p => p.OrderTool)
                    .HasForeignKey(d => d.ToolId)
                    .HasConstraintName("FK_OrderTool_Tool");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Scene>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.TimeEnd).HasColumnType("datetime");

                entity.Property(e => e.TimeStart).HasColumnType("datetime");
            });

            modelBuilder.Entity<SceneCharacter>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.ActorId).HasMaxLength(50);

                entity.Property(e => e.Character).HasMaxLength(50);

                entity.Property(e => e.SceneId).HasMaxLength(50);

                entity.Property(e => e.ScriptLink).IsUnicode(false);

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.SceneCharacter)
                    .HasForeignKey(d => d.ActorId)
                    .HasConstraintName("FK_SceneCharacter_Actor");

                entity.HasOne(d => d.Scene)
                    .WithMany(p => p.SceneCharacter)
                    .HasForeignKey(d => d.SceneId)
                    .HasConstraintName("FK_SceneCharacter_Scene");
            });

            modelBuilder.Entity<Tool>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(10);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(10);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_User");
            });
        }
    }
}
