using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class Einsatz
{
    public int Id { get; set; }

    public int DauerMinuten { get; set; }

    public int? EinsatzArt_Id { get; set; }

    public int Aufrufe { get; set; }

    public DateTime DatumAlamierung { get; set; }

    public string? Beschreibung { get; set; }

    public string? Ueberschrift { get; set; }

    public string? Strasse { get; set; }

    public string? GeoLocation { get; set; }

    public int? Ort_Id { get; set; }

    public bool deleted { get; set; }

    public DateTime created { get; set; }

    public int MdbId { get; set; }

    public string? von { get; set; }

    public virtual EinsatzArt? EinsatzArt { get; set; }

    public virtual ICollection<EinsatzBild> EinsatzBild { get; set; } = new List<EinsatzBild>();

    public virtual ICollection<FahrzeugEinsatz> FahrzeugEinsatz { get; set; } = new List<FahrzeugEinsatz>();

    public virtual Ort? Ort { get; set; }
}
