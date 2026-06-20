using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class ArtikelKategorie
{
    public int id_artikel { get; set; }

    public int id { get; set; }

    public string kategorie { get; set; } = null!;

    public virtual Artikel id_artikelNavigation { get; set; } = null!;
}
