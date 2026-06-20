using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class Fahrzeug
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Tag { get; set; }

    public int SortOrder { get; set; }

    public string? Bez { get; set; }

    public bool? aktiv { get; set; }

    public virtual ICollection<FahrzeugDaten> FahrzeugDaten { get; set; } = new List<FahrzeugDaten>();

    public virtual ICollection<FahrzeugEinsatz> FahrzeugEinsatz { get; set; } = new List<FahrzeugEinsatz>();
}
