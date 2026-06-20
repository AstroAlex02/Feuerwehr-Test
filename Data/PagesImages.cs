using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class PagesImages
{
    public int Id { get; set; }

    public byte[] Image { get; set; } = null!;

    public string Filename { get; set; } = null!;

    public DateTime? Created { get; set; }

    public string? ext { get; set; }
}
