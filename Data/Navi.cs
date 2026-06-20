using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class Navi
{
    public int id { get; set; }

    public string? text { get; set; }

    public string? url { get; set; }

    public int? id_parent { get; set; }

    public int? sortierung { get; set; }

    public string? text_lang { get; set; }

    public string? bild { get; set; }

    public string? gruppe { get; set; }

    public string? title { get; set; }
}
