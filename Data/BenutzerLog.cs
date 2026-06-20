using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class BenutzerLog
{
    public int id { get; set; }

    public int id_benutzer { get; set; }

    public DateTime? datum { get; set; }

    public string? aktion { get; set; }

    public virtual Benutzer id_benutzerNavigation { get; set; } = null!;
}
