using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ffw.Data;

public partial class ffwDb : DbContext
{
    public ffwDb()
    {
    }

    public ffwDb(DbContextOptions<ffwDb> options)
        : base(options)
    {
    }

    public virtual DbSet<Artikel> Artikel { get; set; }

    public virtual DbSet<ArtikelBild> ArtikelBild { get; set; }

    public virtual DbSet<ArtikelKategorie> ArtikelKategorie { get; set; }

    public virtual DbSet<Benutzer> Benutzer { get; set; }

    public virtual DbSet<BenutzerLog> BenutzerLog { get; set; }

    public virtual DbSet<Einsatz> Einsatz { get; set; }

    public virtual DbSet<EinsatzArt> EinsatzArt { get; set; }

    public virtual DbSet<EinsatzBild> EinsatzBild { get; set; }

    public virtual DbSet<Einstellungen> Einstellungen { get; set; }

    public virtual DbSet<Fahrzeug> Fahrzeug { get; set; }

    public virtual DbSet<FahrzeugDaten> FahrzeugDaten { get; set; }

    public virtual DbSet<FahrzeugEinsatz> FahrzeugEinsatz { get; set; }

    public virtual DbSet<Navi> Navi { get; set; }

    public virtual DbSet<Newsletter> Newsletter { get; set; }

    public virtual DbSet<Ort> Ort { get; set; }

    public virtual DbSet<Pages> Pages { get; set; }

    public virtual DbSet<PagesImages> PagesImages { get; set; }

    public virtual DbSet<StatsJahrArt> StatsJahrArt { get; set; }

    public virtual DbSet<Termine> Termine { get; set; }

    public virtual DbSet<Wetter> Wetter { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=feuerwehr");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Artikel>(entity =>
        {
            entity.HasIndex(e => e.Datum, "NonClusteredIndex-20180212-202147");

            entity.Property(e => e.Ueberschrift).HasMaxLength(350);
            entity.Property(e => e.Von).HasMaxLength(50);
        });

        modelBuilder.Entity<ArtikelBild>(entity =>
        {
            entity.HasIndex(e => e.Id_Artikel, "NonClusteredIndex-20180212-202615");

            entity.HasOne(d => d.Id_ArtikelNavigation).WithMany(p => p.ArtikelBild)
                .HasForeignKey(d => d.Id_Artikel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArtikelBild_Artikel");
        });

        modelBuilder.Entity<ArtikelKategorie>(entity =>
        {
            entity.Property(e => e.kategorie).HasMaxLength(50);

            entity.HasOne(d => d.id_artikelNavigation).WithMany(p => p.ArtikelKategorie)
                .HasForeignKey(d => d.id_artikel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArtikelKategorie_Artikel");
        });

        modelBuilder.Entity<Benutzer>(entity =>
        {
            entity.Property(e => e.email).HasMaxLength(50);
            entity.Property(e => e.name).HasMaxLength(150);
            entity.Property(e => e.passwort).HasMaxLength(50);
            entity.Property(e => e.username).HasMaxLength(50);
        });

        modelBuilder.Entity<BenutzerLog>(entity =>
        {
            entity.Property(e => e.aktion).HasMaxLength(350);

            entity.HasOne(d => d.id_benutzerNavigation).WithMany(p => p.BenutzerLog)
                .HasForeignKey(d => d.id_benutzer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BenutzerLog_Benutzer");
        });

        modelBuilder.Entity<Einsatz>(entity =>
        {
            entity.HasIndex(e => e.DatumAlamierung, "NonClusteredIndex-20180212-202205");

            entity.Property(e => e.von).HasMaxLength(50);

            entity.HasOne(d => d.EinsatzArt).WithMany(p => p.Einsatz)
                .HasForeignKey(d => d.EinsatzArt_Id)
                .HasConstraintName("FK_Einsatz_EinsatzArt");

            entity.HasOne(d => d.Ort).WithMany(p => p.Einsatz)
                .HasForeignKey(d => d.Ort_Id)
                .HasConstraintName("FK_Einsatz_Ort");
        });

        modelBuilder.Entity<EinsatzBild>(entity =>
        {
            entity.HasIndex(e => e.Id_Einsatz, "NonClusteredIndex-20180212-202630");

            entity.HasOne(d => d.Id_EinsatzNavigation).WithMany(p => p.EinsatzBild)
                .HasForeignKey(d => d.Id_Einsatz)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EinsatzBild_Einsatz");
        });

        modelBuilder.Entity<Fahrzeug>(entity =>
        {
            entity.Property(e => e.Bez).HasMaxLength(50);
        });

        modelBuilder.Entity<FahrzeugDaten>(entity =>
        {
            entity.Property(e => e.bez).HasMaxLength(250);
            entity.Property(e => e.gruppe).HasMaxLength(50);
            entity.Property(e => e.gruppe_unter).HasMaxLength(50);
            entity.Property(e => e.wert).HasMaxLength(250);

            entity.HasOne(d => d.id_fahrzeugNavigation).WithMany(p => p.FahrzeugDaten)
                .HasForeignKey(d => d.id_fahrzeug)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FahrzeugDaten_Fahrzeug");
        });

        modelBuilder.Entity<FahrzeugEinsatz>(entity =>
        {
            entity.HasOne(d => d.Einsatz).WithMany(p => p.FahrzeugEinsatz)
                .HasForeignKey(d => d.Einsatz_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FahrzeugEinsatz_Einsatz");

            entity.HasOne(d => d.Fahrzeug).WithMany(p => p.FahrzeugEinsatz)
                .HasForeignKey(d => d.Fahrzeug_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FahrzeugEinsatz_Fahrzeug");
        });

        modelBuilder.Entity<Navi>(entity =>
        {
            entity.Property(e => e.bild).HasMaxLength(50);
            entity.Property(e => e.gruppe).HasMaxLength(50);
            entity.Property(e => e.sortierung).HasDefaultValue(0, "DF_Navi_sortierung");
            entity.Property(e => e.text).HasMaxLength(50);
            entity.Property(e => e.text_lang).HasMaxLength(150);
            entity.Property(e => e.title).HasMaxLength(100);
            entity.Property(e => e.url).HasMaxLength(250);
        });

        modelBuilder.Entity<Newsletter>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_Newsletter").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Nachname).HasMaxLength(50);
            entity.Property(e => e.Secret).HasMaxLength(50);
            entity.Property(e => e.Vorname).HasMaxLength(50);
        });

        modelBuilder.Entity<Pages>(entity =>
        {
            entity.HasKey(e => e.url);

            entity.Property(e => e.url).HasMaxLength(300);
            entity.Property(e => e.Created).HasDefaultValueSql("(getdate())", "DF_Pages_Created");
            entity.Property(e => e.Ueberschrift).HasMaxLength(250);
        });

        modelBuilder.Entity<PagesImages>(entity =>
        {
            entity.Property(e => e.Filename).HasMaxLength(150);
            entity.Property(e => e.ext).HasMaxLength(50);
        });

        modelBuilder.Entity<StatsJahrArt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("StatsJahrArt");
        });

        modelBuilder.Entity<Termine>(entity =>
        {
            entity.HasIndex(e => e.Datum, "NonClusteredIndex-20180212-202233");

            entity.Property(e => e.Ort).HasMaxLength(50);
            entity.Property(e => e.TerminTyp).HasMaxLength(50);
            entity.Property(e => e.Ueberschrift).HasMaxLength(250);
        });

        modelBuilder.Entity<Wetter>(entity =>
        {
            entity.Property(e => e.UV).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.baromin).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.dailyrainin).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.dewptf).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.humidity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.indoorhumidity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.indoortempf).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.rainin).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.solarradiation).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.tempf).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.winddir).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.windgustmph).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.windspeedmph).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
