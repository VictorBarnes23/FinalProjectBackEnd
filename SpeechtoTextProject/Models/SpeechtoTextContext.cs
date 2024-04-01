using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SpeechtoTextProject.Models;

public partial class SpeechtoTextContext : DbContext
{
    public SpeechtoTextContext()
    {
    }

    public SpeechtoTextContext(DbContextOptions<SpeechtoTextContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FavoriteWord> FavoriteWords { get; set; }

    public virtual DbSet<UsersTable> UsersTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(Secret.connection_string);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavoriteWord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3214EC0784705BC0");

            entity.Property(e => e.AudioSource).HasMaxLength(400);
            entity.Property(e => e.Context).HasMaxLength(4000);
            entity.Property(e => e.Definition).HasMaxLength(2000);
            entity.Property(e => e.Phonetics).HasMaxLength(100);
            entity.Property(e => e.Source).HasMaxLength(255);
            entity.Property(e => e.UserId).HasMaxLength(255);
            entity.Property(e => e.Word).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.FavoriteWords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FavoriteW__UserI__5DCAEF64");
        });

        modelBuilder.Entity<UsersTable>(entity =>
        {
            entity.HasKey(e => e.GoogleId).HasName("PK__UsersTab__A6FBF2FA62683924");

            entity.ToTable("UsersTable");

            entity.Property(e => e.GoogleId).HasMaxLength(255);
            entity.Property(e => e.LangPreference).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
