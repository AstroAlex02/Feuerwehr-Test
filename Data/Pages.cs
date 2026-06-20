using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class Pages
{
    public string url { get; set; } = null!;

    public DateTime Created { get; set; }

    public string? Inhalt { get; set; }

    public string? Ueberschrift { get; set; }
}
