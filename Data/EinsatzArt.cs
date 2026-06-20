using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class EinsatzArt
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Tag { get; set; }

    public string? Icon { get; set; }

    public int SortOrder { get; set; }

    public virtual ICollection<Einsatz> Einsatz { get; set; } = new List<Einsatz>();
}
