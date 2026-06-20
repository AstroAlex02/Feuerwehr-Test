using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class Artikel
{
    public int Id { get; set; }

    public string? Von { get; set; }

    public string? Inhalt { get; set; }

    public string? Ueberschrift { get; set; }

    public DateTime Datum { get; set; }

    public DateTime? Created { get; set; }

    public bool Deleted { get; set; }

    public int Hits { get; set; }

    public int? mdbId { get; set; }

    public virtual ICollection<ArtikelBild> ArtikelBild { get; set; } = new List<ArtikelBild>();

    public virtual ICollection<ArtikelKategorie> ArtikelKategorie { get; set; } = new List<ArtikelKategorie>();
}
