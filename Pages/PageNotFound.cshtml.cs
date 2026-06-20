using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ffw.Pages;

public class NotFoundModel : MasterPage
{



    public string Path { get; private set; } = "/";

    public void OnGet()
    {
        NewTile = "404 Seite nicht gefunden - ";

        Path = HttpContext?.Request?.Path.ToString() ?? "/";
    }
}
