using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Practice.Models;

public partial class PracticeContext : DbContext
{
    public PracticeContext()
    {
    }

    public PracticeContext(DbContextOptions<PracticeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pair> Pairs { get; set; }

    public virtual DbSet<People> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-65HC71K;Database=Practice;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pair>(entity =>
        {
            entity.HasKey(e => e.PairsId);

            entity.Property(e => e.PairsId).HasColumnName("PairsID");
            entity.Property(e => e.Data).HasColumnType("date");
            entity.Property(e => e.FirstPersonId).HasColumnName("FirstPersonID");
            entity.Property(e => e.SecondPersonId).HasColumnName("SecondPersonID");

            entity.HasOne(d => d.FirstPerson).WithMany(p => p.PairFirstPeople)
                .HasForeignKey(d => d.FirstPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pairs_People");

            entity.HasOne(d => d.SecondPerson).WithMany(p => p.PairSecondPeople)
                .HasForeignKey(d => d.SecondPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pairs_People1");
        });

        modelBuilder.Entity<People>(entity =>
        {
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
