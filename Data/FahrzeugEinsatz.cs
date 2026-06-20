using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class FahrzeugEinsatz
{
    public int Fahrzeug_Id { get; set; }

    public int Einsatz_Id { get; set; }

    public int Id { get; set; }

    public virtual Einsatz Einsatz { get; set; } = null!;

    public virtual Fahrzeug Fahrzeug { get; set; } = null!;
}
