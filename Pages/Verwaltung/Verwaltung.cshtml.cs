using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace ffw.Pages;

public class VerwaltungModel : MasterPage
{
    private readonly IConfiguration _config;

    public VerwaltungModel(IConfiguration config)
    {
        _config = config;
    }

    public string DisplayName { get; set; }

    public bool IsAuthenticated { get; set; }

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!Request.Cookies.TryGetValue("AdminAuth", out var id) || string.IsNullOrEmpty(id))
        {
            IsAuthenticated = false;
            return Page();
        }

        var connStr = _config.GetConnectionString("feuerwehr");
        if (string.IsNullOrEmpty(connStr))
        {
            IsAuthenticated = false;
            return Page();
        }

        try
        {
            await using var conn = new SqlConnection(connStr);
            await conn.OpenAsync();

            const string sql = @"SELECT TOP (1) [id],[username],[name],[deleted]
                                 FROM [ffw].[dbo].[Benutzer]
                                 WHERE [id] = @id";

            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            await using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
            {
                Response.Cookies.Delete("AdminAuth");
                IsAuthenticated = false;
                return Page();
            }

            var deletedObj = reader["deleted"];
            var name = reader["name"]?.ToString();
            var username = reader["username"]?.ToString();

            var isDeleted = false;
            if (deletedObj != null && int.TryParse(deletedObj.ToString(), out var delVal))
            {
                isDeleted = delVal == 1;
            }

            if (isDeleted)
            {
                Response.Cookies.Delete("AdminAuth");
                IsAuthenticated = false;
                return Page();
            }

            DisplayName = !string.IsNullOrEmpty(name) ? name : username;
            IsAuthenticated = true;
            return Page();
        }
        catch
        {
            Response.Cookies.Delete("AdminAuth");
            IsAuthenticated = false;
            return Page();
        }
    }

    public async Task<IActionResult> OnPostLoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Benutzername und Passwort sind erforderlich.";
            return Page();
        }

        var connStr = _config.GetConnectionString("feuerwehr");
        if (string.IsNullOrEmpty(connStr))
        {
            ErrorMessage = "Datenbankverbindung nicht konfiguriert.";
            return Page();
        }

        try
        {
            await using var conn = new SqlConnection(connStr);
            await conn.OpenAsync();

            const string sql = @"SELECT TOP (1) [id],[username],[passwort],[deleted],[name]
                                 FROM [ffw].[dbo].[Benutzer]
                                 WHERE [username] = @username";

            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", Username);

            await using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
            {
                ErrorMessage = "Ungültiger Benutzername oder Passwort.";
                return Page();
            }

            var id = reader["id"]?.ToString();
            var dbPassword = reader["passwort"]?.ToString();
            var deletedObj = reader["deleted"];
            var name = reader["name"]?.ToString();

            var isDeleted = false;
            if (deletedObj != null && int.TryParse(deletedObj.ToString(), out var delVal))
            {
                isDeleted = delVal == 1;
            }

            if (isDeleted)
            {
                ErrorMessage = "Dieser Benutzer ist gesperrt und kann sich nicht anmelden.";
                return Page();
            }

            // Achtung: Hier wird angenommen, dass das Passwort als Klartext in der DB steht.
            // Falls gehashte Passwörter verwendet werden, muss hier die Hash-Prüfung erfolgen.
            if (dbPassword != Password)
            {
                ErrorMessage = "Ungültiger Benutzername oder Passwort.";
                return Page();
            }

            if (!string.IsNullOrEmpty(id))
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = Request.IsHttps,
                    SameSite = SameSiteMode.Strict,
                    Path = "/"
                };
                Response.Cookies.Append("AdminAuth", id, cookieOptions);
            }

            return Redirect("/verwaltung");
        }
        catch (System.Exception ex)
        {
            ErrorMessage = "Fehler bei der Anmeldung: " + ex.Message;
            return Page();
        }
    }

    public IActionResult OnPostLogout()
    {
        Response.Cookies.Delete("AdminAuth");
        return Redirect("/verwaltung");
    }
}
