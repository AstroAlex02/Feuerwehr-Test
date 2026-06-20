using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class Termine
{
    public int Id { get; set; }

    public DateTime Datum { get; set; }

    public string? Ort { get; set; }

    public string? Beschreibung { get; set; }

    public string? Ueberschrift { get; set; }

    public string? TerminTyp { get; set; }

    public int mdbid { get; set; }
}
