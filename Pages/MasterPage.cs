using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ffw.Pages;

public class MasterPage : PageModel
{
    public string NewTile = "";

    [ViewData]
    public string Title
    {
        get
        {
            if (!string.IsNullOrEmpty(NewTile)) { return NewTile; }
            return MainData.GetTitle(HttpContext);
        }
    }


}
