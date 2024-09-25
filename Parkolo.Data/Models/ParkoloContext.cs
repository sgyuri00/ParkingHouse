// <copyright file="ParkoloContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#nullable disable

namespace Parkolo.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;

    /// <summary>
    /// Context class.
    /// </summary>
    public partial class ParkoloContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParkoloContext"/> class.
        /// make sure database is created.
        /// </summary>
        public ParkoloContext()
        {
            this.Database.EnsureCreated();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParkoloContext"/> class.
        /// Parkolo context.
        /// </summary>
        /// <param name="options">options.</param>
        public ParkoloContext(DbContextOptions<ParkoloContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets for Auto entites.
        /// </summary>
        public virtual DbSet<Auto> Autos { get; set; }

        /// <summary>
        /// Gets or sets for Parkolas entites.
        /// </summary>
        public virtual DbSet<Parkolas> Parkolas { get; set; }

        /// <summary>
        /// Gets or sets for Parkolo entites.
        /// </summary>
        public virtual DbSet<ParkoloSpots> Parkolos { get; set; }

        /// <summary>
        /// Gets or sets for Szemely entites.
        /// </summary>
        public virtual DbSet<Szemely> Szemelies { get; set; }

        /// <summary>
        /// Configuring.
        /// </summary>
        /// <param name="optionsBuilder">option builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\ParkoloDatabase.mdf;Integrated Security=True");
            }
        }

        /// <summary>
        /// Moded builder.
        /// </summary>
        /// <param name="modelBuilder">model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Auto>(entity =>
            {
                entity.HasKey(e => e.Rendszam)
                    .HasName("auto_pk");

                entity.ToTable("Auto");

                entity.Property(e => e.Rendszam)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.GyartasiEv)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("Gyartasi_ev");

                entity.Property(e => e.Marka)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Uzemanyag)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Parkolas>(entity =>
            {
                entity.HasKey(e => e.ParkoloId)
                    .HasName("Parkolas_pk");

                entity.Property(e => e.ParkoloId)
                    .HasColumnType("numeric(20, 0)")
                    .HasColumnName("Parkolo_ID");

                entity.Property(e => e.EltoltottIdo)
                    .HasColumnType("numeric(24, 0)")
                    .HasColumnName("Eltoltott_ido");

                entity.Property(e => e.Koltseg).HasColumnType("numeric(30, 0)");

                entity.Property(e => e.ParkolohelySzam)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Parkolohely_szam");

                entity.Property(e => e.Rendszam)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.SzemelyId)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("Szemely_ID");

                entity.HasOne(d => d.ParkolohelySzamNavigation)
                    .WithMany(p => p.Parkolas)
                    .HasForeignKey(d => d.ParkolohelySzam)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("parkolohely_szam_fk");

                entity.HasOne(d => d.RendszamNavigation)
                    .WithMany(p => p.Parkolas)
                    .HasForeignKey(d => d.Rendszam)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rendszam_fk");

                entity.HasOne(d => d.Szemely)
                    .WithMany(p => p.Parkolas)
                    .HasForeignKey(d => d.SzemelyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("szemely_id_fk");
            });

            modelBuilder.Entity<ParkoloSpots>(entity =>
            {
                entity.HasKey(e => e.ParkolohelySzam)
                    .HasName("Parkolo_pk");

                entity.ToTable("Parkolo");

                entity.Property(e => e.ParkolohelySzam)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Parkolohely_szam");

                entity.Property(e => e.ElektromosE)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("Elektromos_E");

                entity.Property(e => e.Meret).HasColumnType("numeric(15, 0)");
            });

            modelBuilder.Entity<Szemely>(entity =>
            {
                entity.ToTable("Szemely");

                entity.Property(e => e.SzemelyId)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("Szemely_ID");

                entity.Property(e => e.Nem)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nev)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SzuletesiIdo)
                    .HasColumnType("datetime")
                    .HasColumnName("Szuletesi_ido");
            });

            this.OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
