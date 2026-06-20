using Microsoft.AspNetCore.Mvc;

namespace ffw.Pages.Components;

public class NavigationViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var items = MainData.GetNavigationData();

        var model = items
            .Select(mi => (mi.Path, mi.Text, isActive: MainData.IsActive(mi.Path, HttpContext)))
            .ToList();

        return View(model);
    }

  
}
