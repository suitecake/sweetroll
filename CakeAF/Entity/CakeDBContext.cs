using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CakeAF.Entity
{
    public partial class CakeDBContext : DbContext
    {
        public CakeDBContext()
        {
        }

        public CakeDBContext(DbContextOptions<CakeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Champion> Champions { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Pentakill> Pentakills { get; set; }
        public virtual DbSet<SoloRecord> SoloRecords { get; set; }
        public virtual DbSet<TeamRecord> TeamRecords { get; set; }
        public virtual DbSet<TeamRecordMember> TeamRecordMembers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["ConnectionString"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Champion>(entity =>
            {
                entity.ToTable("Champion");

                entity.HasIndex(e => e.InternalName, "UC_Champion_InternalName")
                    .IsUnique();

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.InternalName)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.ToTable("Friend");

                entity.HasIndex(e => e.Name, "UC_Friend_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.HasIndex(e => e.Puuid, "UC_Member_PUUID")
                    .IsUnique();

                entity.HasIndex(e => e.SummonerName, "UC_Member_SummonerName")
                    .IsUnique();

                entity.Property(e => e.Puuid)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("PUUID");

                entity.Property(e => e.SummonerName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Friend)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.FriendId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Member_Friend");
            });

            modelBuilder.Entity<Pentakill>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Pentakill");
            });

            modelBuilder.Entity<SoloRecord>(entity =>
            {
                entity.ToTable("SoloRecord");

                entity.HasIndex(e => e.Name, "UC_SoloRecord_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Value).HasColumnType("decimal(15, 5)");

                entity.HasOne(d => d.Champion)
                    .WithMany(p => p.SoloRecords)
                    .HasForeignKey(d => d.ChampionId)
                    .HasConstraintName("FK_SoloRecord_Champion");
            });

            modelBuilder.Entity<TeamRecord>(entity =>
            {
                entity.ToTable("TeamRecord");

                entity.HasIndex(e => e.Name, "UC_TeamRecord_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.Property(e => e.Value).HasColumnType("decimal(15, 5)");
            });

            modelBuilder.Entity<TeamRecordMember>(entity =>
            {
                entity.HasKey(e => new { e.TeamRecordId, e.MemberId });

                entity.ToTable("TeamRecordMember");

                entity.HasOne(d => d.Champion)
                    .WithMany(p => p.TeamRecordMembers)
                    .HasForeignKey(d => d.ChampionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamRecordMember_Champion");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.TeamRecordMembers)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamRecordMember_Member");

                entity.HasOne(d => d.TeamRecord)
                    .WithMany(p => p.TeamRecordMembers)
                    .HasForeignKey(d => d.TeamRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeamRecordMember_TeamRecord");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
