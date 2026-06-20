using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class Benutzer
{
    public int id { get; set; }

    public string username { get; set; } = null!;

    public string passwort { get; set; } = null!;

    public bool deleted { get; set; }

    public string name { get; set; } = null!;

    public string? email { get; set; }

    public bool? artikel { get; set; }

    public bool? einsaetze { get; set; }

    public bool? termine { get; set; }

    public DateTime? lastlogin { get; set; }

    public int logincount { get; set; }

    public virtual ICollection<BenutzerLog> BenutzerLog { get; set; } = new List<BenutzerLog>();
}
