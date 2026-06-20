using System;
using System.Collections.Generic;

namespace ffw.Data;

public partial class Wetter
{
    public long id { get; set; }

    public DateTime created { get; set; }

    public decimal? baromin { get; set; }

    public decimal? tempf { get; set; }

    public decimal? dewptf { get; set; }

    public decimal? humidity { get; set; }

    public decimal? windspeedmph { get; set; }

    public decimal? windgustmph { get; set; }

    public decimal? rainin { get; set; }

    public decimal? dailyrainin { get; set; }

    public decimal? solarradiation { get; set; }

    public decimal? UV { get; set; }

    public decimal? indoortempf { get; set; }

    public decimal? indoorhumidity { get; set; }

    public decimal? winddir { get; set; }
}
