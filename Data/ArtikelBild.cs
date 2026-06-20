using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class ArtikelBild
{
    public int Id { get; set; }

    public int Position { get; set; }

    public string? Beschreibung { get; set; }

    public DateTime Datum { get; set; }

    public string? Link { get; set; }

    public string? Urheber { get; set; }

    public byte[]? Image { get; set; }

    public bool deleted { get; set; }

    public DateTime created { get; set; }

    public int Id_Artikel { get; set; }

    public virtual Artikel Id_ArtikelNavigation { get; set; } = null!;
}
