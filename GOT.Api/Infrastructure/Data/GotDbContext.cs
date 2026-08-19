using System;
using System.Collections.Generic;
using GOT.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace GOT.Api.Infrastructure.Data;


public partial class GotDbContext : DbContext
{
    public GotDbContext()
    {
    }

    public GotDbContext(DbContextOptions<GotDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Battle> Battles { get; set; }

    public virtual DbSet<BattleCommander> BattleCommanders { get; set; }

    public virtual DbSet<BattleHouse> BattleHouses { get; set; }

    public virtual DbSet<BattleKing> BattleKings { get; set; }

    public virtual DbSet<BattleType> BattleTypes { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Death> Deaths { get; set; }

    public virtual DbSet<DeathCategory> DeathCategories { get; set; }

    public virtual DbSet<House> Houses { get; set; }

    public virtual DbSet<Kingdom> Kingdoms { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//  #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB; Database=GotDB; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Battle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Battles_Id");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Notes).IsUnicode(false);

            entity.HasOne(d => d.BattleType).WithMany(p => p.Battles)
                .HasForeignKey(d => d.BattleTypeId)
                .HasConstraintName("FK_Battles_BattleTypeId");

            entity.HasOne(d => d.Location).WithMany(p => p.Battles)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Battles_LocationId");
        });

        modelBuilder.Entity<BattleCommander>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BattleCommanders_Id");

            entity.HasOne(d => d.Battle).WithMany(p => p.BattleCommanders)
                .HasForeignKey(d => d.BattleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BattleCommanders_BattleId");

            entity.HasOne(d => d.Commander).WithMany(p => p.BattleCommanders)
                .HasForeignKey(d => d.CommanderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BattleCommanders_CommanderId");
        });

        modelBuilder.Entity<BattleHouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BattleHouses_Id");

            entity.HasOne(d => d.Battle).WithMany(p => p.BattleHouses)
                .HasForeignKey(d => d.BattleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BattleHouses_BattleId");

            entity.HasOne(d => d.House).WithMany(p => p.BattleHouses)
                .HasForeignKey(d => d.HouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BattleHouses_HouseId");
        });

        modelBuilder.Entity<BattleKing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BattleKings_Id");

            entity.HasOne(d => d.Battle).WithMany(p => p.BattleKings)
                .HasForeignKey(d => d.BattleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BattleKings_BattleId");

            entity.HasOne(d => d.King).WithMany(p => p.BattleKings)
                .HasForeignKey(d => d.KingId)
                .HasConstraintName("FK_BattleKings_KingId");
        });

        modelBuilder.Entity<BattleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BattleTypes_Id");

            entity.Property(e => e.BattleType1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BattleType");
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Chapters_Id");

            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsViewers).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Season).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.SeasonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chapters_SeasonId");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Characters_Id");

            entity.Property(e => e.IsHuman).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Death>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Deaths_Id");

            entity.Property(e => e.Allegiance).IsUnicode(false);
            entity.Property(e => e.DeathCount).HasDefaultValue(1);
            entity.Property(e => e.DeathDescription).IsUnicode(false);
            entity.Property(e => e.LocationComments).IsUnicode(false);
            entity.Property(e => e.Reason)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.Chapter).WithMany(p => p.Deaths)
                .HasForeignKey(d => d.ChapterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deaths_ChapterId");

            entity.HasOne(d => d.DeathCategory).WithMany(p => p.Deaths)
                .HasForeignKey(d => d.DeathCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deaths_DeathCategoryId");

            entity.HasOne(d => d.Killed).WithMany(p => p.DeathKilleds)
                .HasForeignKey(d => d.KilledId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deaths_KilledId");

            entity.HasOne(d => d.Killer).WithMany(p => p.DeathKillers)
                .HasForeignKey(d => d.KillerId)
                .HasConstraintName("FK_Deaths_KillerId");

            entity.HasOne(d => d.Location).WithMany(p => p.Deaths)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_Deaths_LocationId");
        });

        modelBuilder.Entity<DeathCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DeathCategories_Id");

            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Houses_Id");

            entity.Property(e => e.CoatOfArmsUrl)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Summary).IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Kingdom).WithMany(p => p.Houses)
                .HasForeignKey(d => d.KingdomId)
                .HasConstraintName("FK_Houses_KingdomId");
        });

        modelBuilder.Entity<Kingdom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Kingdoms_Id");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Summary).IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Locations_Id");

            entity.Property(e => e.Location1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Location");
            entity.Property(e => e.Summary).IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Kingdom).WithMany(p => p.Locations)
                .HasForeignKey(d => d.KingdomId)
                .HasConstraintName("FK_Locations_KingdomId");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_People_Id");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Seasons_Id");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
