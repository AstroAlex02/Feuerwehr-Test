using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class Newsletter
{
    public int Id { get; set; }

    public string Vorname { get; set; } = null!;

    public string Nachname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Created { get; set; }

    public bool Deleted { get; set; }

    public bool Aktivert { get; set; }

    public string Secret { get; set; } = null!;
}
