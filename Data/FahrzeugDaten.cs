using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class FahrzeugDaten
{
    public int id { get; set; }

    public string? gruppe { get; set; }

    public string? bez { get; set; }

    public int? sortierung { get; set; }

    public string? wert { get; set; }

    public int id_fahrzeug { get; set; }

    public string? gruppe_unter { get; set; }

    public virtual Fahrzeug id_fahrzeugNavigation { get; set; } = null!;
}
