using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.ViewComponents.Footer
{
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
