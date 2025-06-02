using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Final_MozArt.ViewComponents.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {        
            return await Task.FromResult(View());
        }
    }
}
