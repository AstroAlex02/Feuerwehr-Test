using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace ffw.Pages.FeatureArtikel
{
    public class ArtikelItem
    {
        public int Id { get; set; }
        public string Ueberschrift { get; set; } = "";
        public string Inhalt { get; set; } = "";
        public DateTime Datum { get; set; }
        public string Von { get; set; } = "";
        // optional image path or URL for the article (may be empty)
        public string Bild { get; set; } = "";
        public string Slug => MainData.UrlFriendly(Ueberschrift);
    }

    public interface IArtikelRepository
    {
        Task<List<ArtikelItem>> GetAllAsync();
        Task<ArtikelItem?> GetByIdAsync(int id);
    }

    public class ArtikelRepository : IArtikelRepository
    {
        private readonly IConfiguration _config;

        public ArtikelRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<ArtikelItem>> GetAllAsync()
        {
            try
            {
                var connStr = _config.GetConnectionString("feuerwehr");
                if (string.IsNullOrWhiteSpace(connStr)) return new List<ArtikelItem>();

                using var conn = new SqlConnection(connStr);
                await conn.OpenAsync();
                var cmd = conn.CreateCommand();
                // Use explicit Artikel table columns as in your database
                cmd.CommandText = @"
SELECT TOP (1000) [Id],[Von],[Inhalt],[Ueberschrift],[Datum],[Deleted]
FROM [ffw].[dbo].[Artikel]
WHERE COALESCE([Deleted],0) = 0
ORDER BY [Datum] DESC";

                var list = new List<ArtikelItem>();
                using var rdr = await cmd.ExecuteReaderAsync();
                while (await rdr.ReadAsync())
                {
                    var it = new ArtikelItem();
                    // Column order: Id, Von, Inhalt, Ueberschrift, Datum, Deleted
                    it.Id = rdr.IsDBNull(0) ? 0 : Convert.ToInt32(rdr.GetValue(0));
                    it.Von = rdr.IsDBNull(1) ? "" : rdr.GetString(1);
                    it.Inhalt = rdr.IsDBNull(2) ? "" : rdr.GetString(2);
                    it.Ueberschrift = rdr.IsDBNull(3) ? "(kein Titel)" : rdr.GetString(3);
                    if (!rdr.IsDBNull(4))
                    {
                        var val = rdr.GetValue(4);
                        if (val is DateTime dt) it.Datum = dt;
                        else { if (DateTime.TryParse(val?.ToString(), out var p)) it.Datum = p; }
                    }
                    // try to read optional image column(s) if present
                    try
                    {
                        // common column names: Bild, Image, BildPfad, ImagePath
                        string? img = null;
                        if (ColumnExists(rdr, "Bild")) img = rdr.IsDBNull(rdr.GetOrdinal("Bild")) ? null : rdr.GetString(rdr.GetOrdinal("Bild"));
                        else if (ColumnExists(rdr, "Image")) img = rdr.IsDBNull(rdr.GetOrdinal("Image")) ? null : rdr.GetString(rdr.GetOrdinal("Image"));
                        else if (ColumnExists(rdr, "BildPfad")) img = rdr.IsDBNull(rdr.GetOrdinal("BildPfad")) ? null : rdr.GetString(rdr.GetOrdinal("BildPfad"));
                        else if (ColumnExists(rdr, "ImagePath")) img = rdr.IsDBNull(rdr.GetOrdinal("ImagePath")) ? null : rdr.GetString(rdr.GetOrdinal("ImagePath"));
                        if (!string.IsNullOrWhiteSpace(img)) it.Bild = img!;
                    }
                    catch { }
                    // read Deleted but ignore here because query already filters deleted rows
                    // var deleted = rdr.IsDBNull(5) ? 0 : Convert.ToInt32(rdr.GetValue(5));
                    list.Add(it);
                }
                return list;
            }
            catch
            {
                return new List<ArtikelItem>();
            }
        }

        public async Task<ArtikelItem?> GetByIdAsync(int id)
        {
            try
            {
                var connStr = _config.GetConnectionString("feuerwehr");
                if (string.IsNullOrWhiteSpace(connStr)) return null;

                using var conn = new SqlConnection(connStr);
                await conn.OpenAsync();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"
SELECT TOP(1) [Id],[Von],[Inhalt],[Ueberschrift],[Datum],[Deleted]
FROM [ffw].[dbo].[Artikel]
WHERE [Id] = @id";
                var p = cmd.CreateParameter();
                p.ParameterName = "@id";
                p.Value = id;
                cmd.Parameters.Add(p);

                using var rdr = await cmd.ExecuteReaderAsync();
                if (await rdr.ReadAsync())
                {
                    var it = new ArtikelItem();
                    // Column order: Id, Von, Inhalt, Ueberschrift, Datum
                    it.Id = rdr.IsDBNull(0) ? 0 : Convert.ToInt32(rdr.GetValue(0));
                    it.Von = rdr.IsDBNull(1) ? "" : rdr.GetString(1);
                    it.Inhalt = rdr.IsDBNull(2) ? "" : rdr.GetString(2);
                    it.Ueberschrift = rdr.IsDBNull(3) ? "(kein Titel)" : rdr.GetString(3);
                    if (!rdr.IsDBNull(4))
                    {
                        var val = rdr.GetValue(4);
                        if (val is DateTime dt) it.Datum = dt;
                        else { if (DateTime.TryParse(val?.ToString(), out var p2)) it.Datum = p2; }
                    }
                    try
                    {
                        if (ColumnExists(rdr, "Bild")) it.Bild = rdr.IsDBNull(rdr.GetOrdinal("Bild")) ? "" : rdr.GetString(rdr.GetOrdinal("Bild"));
                        else if (ColumnExists(rdr, "Image")) it.Bild = rdr.IsDBNull(rdr.GetOrdinal("Image")) ? "" : rdr.GetString(rdr.GetOrdinal("Image"));
                        else if (ColumnExists(rdr, "BildPfad")) it.Bild = rdr.IsDBNull(rdr.GetOrdinal("BildPfad")) ? "" : rdr.GetString(rdr.GetOrdinal("BildPfad"));
                        else if (ColumnExists(rdr, "ImagePath")) it.Bild = rdr.IsDBNull(rdr.GetOrdinal("ImagePath")) ? "" : rdr.GetString(rdr.GetOrdinal("ImagePath"));
                    }
                    catch { }
                    return it;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        private static bool ColumnExists(SqlDataReader rdr, string columnName)
        {
            try
            {
                return rdr.GetOrdinal(columnName) >= 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
