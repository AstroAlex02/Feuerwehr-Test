using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class Ort
{
    public int Id { get; set; }

    public string? PLZ { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Einsatz> Einsatz { get; set; } = new List<Einsatz>();
}
