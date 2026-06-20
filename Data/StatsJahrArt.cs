using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class StatsJahrArt
{
    public long RowID { get; set; }

    public string? Art { get; set; }

    public int? Jahr { get; set; }

    public int? Anzahl { get; set; }
}
